using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace HomeCinema
{
    public static class AnilistAPI
    {
        private static readonly string Host = "https://graphql.anilist.co";
        private static readonly string Query = @"query ($search: String) {
            Page (page: 1, perPage: 25) {
                pageInfo {
                    total
                    currentPage
                    lastPage
                    hasNextPage
                    perPage
                }
                media (type: ANIME, search: $search) {
                    id format episodes
                    title { romaji english }
                    coverImage { medium }
                }
            }
        }";
        private static readonly string QueryWithId = @"query ($Id: Int) {
            Media (id: $Id, type: ANIME) {
                id format episodes genres countryOfOrigin description
                trailer { id site }
                studios {
                    edges { isMain node { id name } }
                }
                staff {
                    edges {
                    role
                    node { name { full } }
                    }
                }
                title { english romaji }
                startDate { year month day }
                coverImage { medium large }
            }
        }";

        public static AnilistPageQuery SearchForAnime(string text)
        {
            string errFrom = "AnilistAPI-SearchForAnime";
            int retry = 3;
            while (retry > 0)
            {
                try
                {
                    var client = new RestClient(Host);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
                    
                    var request = new RestRequest("/", Method.POST);
                    request.Timeout = 0;
                    request.AddJsonBody(new
                    {
                        query = Query,
                        variables = "{ \"search\": \"" + text.Replace("\"", string.Empty) + "\"}"
                    });
                    GlobalVars.LogDebug($"Retry({retry-1}), Will search: [{text}]");
                    var execute = client.Execute(request);
                    GlobalVars.LogDebug($"Finished search: [{text}]");
                    if (execute.Headers != null)
                    {
                        if (int.TryParse(execute.Headers.ToList().Find(x => x.Name == "Retry-After")?.Value.ToString(), out int val))
                        {
                            GlobalVars.LogDebug($"Retry-After: [{val}]");
                            if (val > 0)
                            {
                                GlobalVars.LogDebug($"Sleeping thread for 60000ms(1 minute)");
                                Thread.Sleep(val*1000);
                                continue;
                            }
                        }
                    }
                    if (execute.StatusCode == HttpStatusCode.OK)
                    {
                        var result = execute.Content;
                        var jsonObj = JsonConvert.DeserializeObject<AnilistPageQuery>(result, GlobalVars.JSON_SETTING);
                        retry = -1;
                        return jsonObj;
                    }
                    retry -= 1;
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError(errFrom, ex, false);
                    retry -= 1;
                }
            }
            return null;
        }
        public static MediaInfo GetMovieInfoFromAnilist(string Id)
        {
            string calledFrom = "AnilistAPI-GetMovieInfoFromAnilist";
            int retry = 3;
            while (retry > 0)
            {
                try
                {
                    var client = new RestClient(Host);
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;

                    var request = new RestRequest("/", Method.POST);
                    request.Timeout = 0;
                    request.AddJsonBody(new
                    {
                        query = QueryWithId,
                        variables = "{ \"Id\": " + Id + "}"
                    });
                    GlobalVars.LogDebug($"Retry({retry-1}), Query Id: [{Id}]");
                    var execute = client.Execute(request);
                    GlobalVars.LogDebug("Done execute!");
                    // Handle TimeOut for Rate Limiting
                    if (execute.Headers != null)
                    {
                        if (int.TryParse(execute.Headers.ToList().Find(x => x.Name == "Retry-After")?.Value.ToString(), out int val))
                        {
                            GlobalVars.LogDebug($"Retry-After: [{val}]");
                            if (val > 0)
                            {
                                GlobalVars.LogDebug($"Sleeping thread for 60000ms(1 minute)");
                                Thread.Sleep(val*1000);
                                continue;
                            }
                        }
                    }
                    // Break on not 200
                    if (execute.StatusCode != HttpStatusCode.OK)
                    {
                        retry -= 1;
                        continue;
                    }
                    var result = execute.Content;
                    var jsonObj = JsonConvert.DeserializeObject<AnilistSingleQuery>(result, GlobalVars.JSON_SETTING);

                    var media = new MediaInfo();
                    var anime = jsonObj.Data.Media;
                    var studioEdge = anime.Studios.Edge;
                    List<String> studioList = new List<string>();

                    foreach (var node in studioEdge)
                    {
                        try { studioList.Add(node.Node.Name.Trim()); }
                        catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }
                    }

                    if (String.IsNullOrWhiteSpace(anime.Title.English))
                    {
                        media.Title = anime.Title.Romaji;
                    }
                    else
                    {
                        media.Title = anime.Title.English;
                        media.OrigTitle = anime.Title.Romaji;
                    }

                    media.Anilist = anime.Id.ToString();

                    try { media.Episodes = anime.Episodes; }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try { media.Genre = anime.Genres; }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try { media.Summary = anime.Description; }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try { media.ReleaseDate = $"{anime.StartDate.Year}-{anime.StartDate.Month}-{anime.StartDate.Day}"; }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try { media.PosterPath = anime.CoverImage.Large; }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try { media.Studio = GlobalVars.ConvertListToString(studioList, ",", calledFrom); }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try
                    {
                        string trailersite = anime.Trailer?.Site;
                        if (!String.IsNullOrWhiteSpace(trailersite))
                        {
                            media.Trailer = (trailersite.ToLower().Contains("youtube")) ? GlobalVars.LINK_YT + anime.Trailer.Id : "";
                        }
                    }
                    catch (Exception ex) { GlobalVars.ShowError(calledFrom, ex, false); }

                    try
                    {
                        var Region = new RegionInfo(anime.CountryOfOrigin);
                        string country = (!String.IsNullOrWhiteSpace(Region.EnglishName)) ? Region.EnglishName : Region.DisplayName;
                        if (!String.IsNullOrWhiteSpace(country))
                        {
                            media.Country.Add(country);
                        }
                    }
                    catch { }

                    foreach (var prod in anime.Staff.Edge)
                    {
                        if (prod.Role.Trim().ToLower().Equals("producer"))
                        {
                            media.Producer = prod.Node.Name.Full.Trim();
                        }
                        if (prod.Role.Trim().ToLower().Equals("director"))
                        {
                            media.Director = prod.Node.Name.Full.Trim();
                        }
                        if (!String.IsNullOrWhiteSpace(media.Producer) && !String.IsNullOrWhiteSpace(media.Director))
                        {
                            break;
                        }
                    }
                    retry = -1;
                    return media;
                }
                catch (Exception ex)
                {
                    GlobalVars.ShowError(calledFrom, ex, false);
                    retry -= 1;
                }
            }
            return null;
        }
        // Download Movie cover image from Anilist
        public static bool DownloadCoverFromAnilist(string MOVIE_ID, string linkPoster, string calledFrom)
        {
            string errFrom = $"GlobalVars-DownloadCoverFromAnilist [calledFrom: {calledFrom}]";
            // Parse image link from JSON and download it
            if (String.IsNullOrWhiteSpace(linkPoster) == false)
            {
                string moviePosterDL = GlobalVars.PATH_TEMP + MOVIE_ID + ".jpg";
                GlobalVars.DeleteMove(moviePosterDL, errFrom); // Delete prev file, if exists
                return GlobalVars.DownloadLoop(moviePosterDL, linkPoster, errFrom, false);
            }
            return false;
        }
    }
}
