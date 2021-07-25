using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace HomeCinema
{
    public static class TmdbAPI
    {
        private static string TMDB_KEY = GlobalVars.TMDB_KEY;

        public static TmdbSearchResult FindMovieTV(string query, string MovieId, string mediatype, bool showAMsg = false)
        {
            TmdbSearchResult media = null;
            string errFrom = $"TmdbAPI-FindMovieTV";
            string mediatypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            string Searchquery = System.Net.WebUtility.UrlEncode(query);
            // URLs
            string URLFindMedia = @"https://api.themoviedb.org/3/search/" + $"{mediatypeUrl}?api_key={TMDB_KEY}&query={Searchquery}";
            // Filepaths
            string FileFindMedia = GlobalVars.PATH_TEMP + MovieId + "_findMedia.json";

            if (GlobalVars.DownloadAndReplace(FileFindMedia, URLFindMedia, errFrom, showAMsg))
            {
                string content = GlobalVars.ReadStringFromFile(FileFindMedia, errFrom);
                media = JsonConvert.DeserializeObject<TmdbSearchResult>(content);
            }
            GlobalVars.TryDelete(FileFindMedia, errFrom);
            return media;
        }
        public static MediaInfo GetMovieInfoFromTmdb(string Tmdb, string mediatype)
        {
            MediaInfo media = new MediaInfo();
            string errFrom = $"TmdbAPI-GetMovieInfoFromTmdb";
            string mediaTypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            // URLs
            string URLFindMovie = @"https://api.themoviedb.org/3/" + $"{mediaTypeUrl}/{Tmdb}?api_key={TMDB_KEY}&append_to_response=external_ids";
            string URLFindVids = @"https://api.themoviedb.org/3/" + $"{mediaTypeUrl}/{Tmdb}/videos?api_key={TMDB_KEY}";
            string URLFindCrew = @"https://api.themoviedb.org/3/" + $"{mediaTypeUrl}/{Tmdb}/credits?api_key={TMDB_KEY}";
            // File paths
            string JSONmovieinfo = GlobalVars.PATH_TEMP + Tmdb + "_info.json";
            string JSONmovievids = GlobalVars.PATH_TEMP + Tmdb + "_videos.json";
            string JSONcrewcast = GlobalVars.PATH_TEMP + Tmdb + "_crewcast.json";

            if (!String.IsNullOrWhiteSpace(Tmdb) && Tmdb!="0")
            {
                if (GlobalVars.DownloadAndReplace(JSONmovieinfo, URLFindMovie, errFrom))
                {
                    string content = GlobalVars.ReadStringFromFile(JSONmovieinfo, errFrom);
                    var movie = JsonConvert.DeserializeObject<TmdbMovieInfo>(content);
                    if (movie != null)
                    {
                        media.Imdb = movie.ExternalIds.Imdb;
                        if (mediatype == "movie")
                        {
                            media.Title = movie.Title; // title
                            media.OrigTitle = movie.OrigTitle; // original title
                            media.ReleaseDate = movie.ReleaseDate; // release date
                        }
                        else
                        {
                            media.Title = movie.Name; // series title
                            media.OrigTitle = movie.OrigName;
                            media.ReleaseDate = movie.FirstAirDate; // release date
                            media.SeriesName = movie.OrigName;
                            media.Seasons = movie.SeasonsCount;
                            media.Episodes = movie.EpisodesCount;
                        }
                        media.Summary = movie.Summary; // summary / overview
                        media.PosterPath = (!String.IsNullOrWhiteSpace(movie.PosterPath)) ? movie.PosterPath : String.Empty; // poster_path
                        // Country
                        foreach (TmdbProdCountry c in movie.ProdCountries)
                        {
                            media.Country.Add(c.Name.Trim());
                        }
                        // Get genres
                        if (content.Contains("genres"))
                        {
                            var jObj = (JObject)JsonConvert.DeserializeObject(content);
                            var result = jObj["genres"]
                                            .Select(item => new
                                            {
                                                name = (string)item["name"],
                                            })
                                            .ToList();
                            if (result.Count > 0)
                            {
                                foreach (var valGenre in result)
                                {
                                    string valString = valGenre.ToString();
                                    string valTrim = valString.Substring(valString.IndexOf("name = ") + 7);
                                    valTrim = valTrim.TrimEnd('}');
                                    valTrim.Trim();
                                    media.Genre.Add(valTrim);
                                }
                            }
                        }
                        // Studio
                        try { media.Studio = movie.ProdCompanies.Select(a => a.Name).ToList().Aggregate((b, c) => b + ", " + c); }
                        catch { media.Studio = ""; }
                    }
                }
                // Get Trailer
                if (GlobalVars.DownloadAndReplace(JSONmovievids, URLFindVids, errFrom))
                {
                    // Get contents of JSON file
                    string contents = GlobalVars.ReadStringFromFile(JSONmovievids, errFrom + " [JSONmovievids]");
                    try
                    {
                        var objJson = JsonConvert.DeserializeObject<TmdbVideos>(contents);
                        if (objJson.results[0].site.ToLower() == "youtube")
                        {
                            media.Trailer = objJson.results[0].key;
                        }
                    }
                    catch
                    {
                        media.Trailer = "";
                    }
                }
                // Get Crews and Casts
                if (GlobalVars.DownloadAndReplace(JSONcrewcast, URLFindCrew, errFrom))
                {
                    // Unparse json into object list
                    string contents = GlobalVars.ReadStringFromFile(JSONcrewcast, errFrom + " [JSONcrewcast]");
                    var castcrew = JsonConvert.DeserializeObject<TmdbCastCrew>(contents);

                    string tmp = ""; // temporary string var. Use to get strings
                    string tmpDir = "", tmpProd = ""; // Use to get director and producer

                    foreach (TmdbCast c in castcrew.cast)
                    {
                        // Get informations
                        tmp += c.name + ", ";
                    }
                    // Save to list as artist
                    media.Actor = tmp.Trim().TrimEnd(',');

                    // get Director and producer
                    foreach (TmdbCrew c in castcrew.crew)
                    {
                        // Get informations
                        if (c.job.Trim() == "Director")
                        {
                            tmpDir += c.name + ", ";
                        }
                        else if (c.job.Contains("Producer"))
                        {
                            tmpProd += c.name + ", ";
                        }
                    }
                    // Save to list as director and producer
                    media.Director = tmpDir.Trim().TrimEnd(',');
                    media.Producer = tmpProd.Trim().TrimEnd(',');
                }
            }
            return media;
        }

        // Get IMDB from TMDB API Id
        public static string GetImdbFromAPI(string TmdbId, string mediatype)
        {
            string errFrom = $"GlobalVars-GetImdbFromAPI";
            string urlJSONgetImdb, JSONgetImdb;
            string JSONContents = "";
            string mediaTypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            // Check if MovieID is not empty
            if (String.IsNullOrWhiteSpace(TmdbId) == false)
            {
                // GET IMDB
                urlJSONgetImdb = @"https://api.themoviedb.org/3/" + mediaTypeUrl + "/" + TmdbId + "?api_key=" + TMDB_KEY;
                urlJSONgetImdb += (mediatype != "movie") ? "&append_to_response=external_ids" : ""; // Append external_ids param for non-movie
                JSONgetImdb = $"{GlobalVars.PATH_TEMP}tmdb{TmdbId}_movieInfo.json";

                // Download file if not existing (TO GET IMDB Id)
                if (GlobalVars.DownloadAndReplace(JSONgetImdb, urlJSONgetImdb, errFrom))
                {
                    JSONContents = GlobalVars.ReadStringFromFile(JSONgetImdb, errFrom);
                    GlobalVars.TryDelete(JSONgetImdb, errFrom);
                    try
                    {
                        var objMovieInfo = JsonConvert.DeserializeObject<MovieInfo>(JSONContents);
                        return (mediatype == "movie") ? objMovieInfo.imdb_id : objMovieInfo.external_ids.imdb_id;
                    }
                    catch { return String.Empty; }
                }
            }
            return String.Empty;
        }
        // Download Movie cover image from TMB
        public static bool DownloadCoverFromTMDB(string MOVIE_ID, string linkPoster, string calledFrom)
        {
            string errFrom = $"GlobalVars-DownloadCoverFromTMDB [calledFrom: {calledFrom}]";
            // Parse image link from JSON and download it
            if (String.IsNullOrWhiteSpace(linkPoster) == false)
            {
                string moviePosterDL = GlobalVars.PATH_TEMP + MOVIE_ID + ".jpg";
                GlobalVars.DeleteMove(moviePosterDL, errFrom); // Delete prev file, if exists
                return GlobalVars.DownloadLoop(moviePosterDL, "https://image.tmdb.org/t/p/original/" + linkPoster, errFrom, false);
            }
            return false;
        }
    }
}
