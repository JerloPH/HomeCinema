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

        public static TmdbSearchResult FindMovieTV(string query, string MovieId, string mediatype)
        {
            TmdbSearchResult media = null;
            string errFrom = $"TmdbAPI-FindMovieTV";
            string mediatypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            string Searchquery = System.Net.WebUtility.UrlEncode(query);
            // URLs
            string URLFindMedia = @"https://api.themoviedb.org/3/search/" + $"{mediatypeUrl}?api_key={TMDB_KEY}&query={Searchquery}";
            // Filepaths
            string FileFindMedia = DataFile.PATH_TEMP + MovieId + "_findMedia.json";

            if (GlobalVars.DownloadAndReplace(FileFindMedia, URLFindMedia, errFrom))
            {
                string content = GlobalVars.ReadStringFromFile(FileFindMedia, errFrom);
                media = JsonConvert.DeserializeObject<TmdbSearchResult>(content, GlobalVars.JSON_SETTING);
            }
            GlobalVars.TryDelete(FileFindMedia, errFrom);
            return media;
        }
        public static MediaInfo GetMovieInfoFromTmdb(string Tmdb, string mediatype)
        {
            MediaInfo media = new MediaInfo();
            string errFrom = $"TmdbAPI-GetMovieInfoFromTmdb";
            string mediaTypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            // URLs and filepaths
            string URLFindMovie = @"https://api.themoviedb.org/3/" + $"{mediaTypeUrl}/{Tmdb}?api_key={TMDB_KEY}&append_to_response=external_ids,videos,credits";
            string JSONmovieinfo = $"{DataFile.PATH_TEMP}{Tmdb}_info.json";
            // Validate
            if (!String.IsNullOrWhiteSpace(Tmdb) && Tmdb != "0")
            {
                if (GlobalVars.DownloadAndReplace(JSONmovieinfo, URLFindMovie, errFrom))
                {
                    string content = GlobalVars.ReadStringFromFile(JSONmovieinfo, errFrom);
                    Logs.Debug(DataFile.PATH_LOG+"\\json_debug.json", content);
                    var movie = JsonConvert.DeserializeObject<TmdbMovieInfo>(content, GlobalVars.JSON_SETTING);
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
                        // Studio
                        try { media.Studio = movie.ProdCompanies.Select(a => a.Name).ToList(); }
                        catch { }
                        // Country
                        if (movie.ProdCountries?.Count > 0)
                        {
                            foreach (TmdbProdCountry c in movie.ProdCountries)
                            {
                                media.Country.Add(c.Name.Trim());
                            }
                        }
                        // Get genres
                        if (movie.Genres?.Count > 0)
                        {
                            try
                            {
                                foreach (var item in movie.Genres)
                                {
                                    media.Genre.Add(item.Name);
                                }
                            }
                            catch (Exception ex) { Logs.LogErr(errFrom, ex); }
                        }
                        // Trailer Video
                        if (movie.Videos != null)
                        {
                            if (movie.Videos.results?.Count > 0)
                            {
                                try
                                {
                                    foreach (var item in movie.Videos.results)
                                    {
                                        if (item.site.ToLower().Equals("youtube"))
                                        {
                                            media.Trailer = GlobalVars.LINK_YT + item.key;
                                            break;
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    media.Trailer = "";
                                    Logs.LogErr(errFrom, ex);
                                }
                            }
                        }
                        // Cast and Crews
                        if (movie.CastCrew != null)
                        {
                            if (movie.CastCrew.cast?.Count > 0)
                            {
                                foreach (TmdbCast c in movie.CastCrew.cast)
                                {
                                    media.Casts.Add(c.name.Trim());
                                }
                            }
                            // get Director and producer
                            if (movie.CastCrew.crew?.Count > 0)
                            {
                                foreach (TmdbCrew c in movie.CastCrew.crew)
                                {
                                    string job = c.job.Trim().ToLower(); // Get informations
                                    if (job.Equals("director"))
                                    {
                                        media.Director.Add(c.name.Trim());
                                    }
                                    else if (job.Contains("producer"))
                                    {
                                        media.Producer.Add(c.name.Trim());
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return media;
        }
        // Get IMDB from TMDB API Id
        public static string GetImdbFromAPI(string TmdbId, string mediatype)
        {
            string errFrom = $"TmdbAPI-GetImdbFromAPI";
            string mediaTypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            string JSONgetImdb = $"{DataFile.PATH_TEMP}tmdb{TmdbId}_movieInfo.json";
            string urlJSONgetImdb = @"https://api.themoviedb.org/3/" + $"{mediaTypeUrl}/{TmdbId}?api_key={TMDB_KEY}&append_to_response=external_ids";
            // Check if MovieID is not empty
            if (String.IsNullOrWhiteSpace(TmdbId) == false)
            {
                // Download file if not existing (TO GET IMDB Id)
                if (GlobalVars.DownloadAndReplace(JSONgetImdb, urlJSONgetImdb, errFrom))
                {
                    string JSONContents = GlobalVars.ReadStringFromFile(JSONgetImdb, errFrom);
                    GlobalVars.TryDelete(JSONgetImdb, errFrom);
                    try
                    {
                        var objMovieInfo = JsonConvert.DeserializeObject<TmdbSearchMovieInfo>(JSONContents, GlobalVars.JSON_SETTING);
                        return (mediatype == "movie") ? objMovieInfo.imdb_id : objMovieInfo.external_ids.Imdb;
                    }
                    catch { return String.Empty; }
                }
            }
            return String.Empty;
        }
        public static string GetTmdbFromImdb(string ImdbId, string mediatype)
        {
            string errFrom = $"TmdbAPI-GetTmdbFromImdb";
            string URL = @"https://api.themoviedb.org/3/find/" + $"{ImdbId}?api_key={TMDB_KEY}&language=en-US&external_source=imdb_id";
            string File = $"{DataFile.PATH_TEMP}{ImdbId}_findTmdb.json";
            if (String.IsNullOrWhiteSpace(ImdbId) == false)
            {
                // Download file if not existing (TO GET IMDB Id)
                if (GlobalVars.DownloadAndReplace(File, URL, errFrom))
                {
                    string JSONContents = GlobalVars.ReadStringFromFile(File, errFrom);
                    GlobalVars.TryDelete(File, errFrom);
                    try
                    {
                        var res = JsonConvert.DeserializeObject<TmdbSearchFromImdb>(JSONContents, GlobalVars.JSON_SETTING);
                        var ret = (mediatype.ToLower().Equals("movie") ? res.MovieResults[0].Id : res.TVResults[0].Id);
                        return ret.ToString();
                    }
                    catch { return String.Empty; }
                }
            }
            return String.Empty;
        }
        // Download Movie cover image from TMB
        public static bool DownloadCoverFromTMDB(string MOVIE_ID, string linkPoster, string calledFrom)
        {
            string errFrom = $"TmdbAPI-DownloadCoverFromTMDB [calledFrom: {calledFrom}]";
            // Parse image link from JSON and download it
            if (String.IsNullOrWhiteSpace(linkPoster) == false)
            {
                string moviePosterDL = $"{DataFile.PATH_TEMP}{MOVIE_ID}.jpg";
                GlobalVars.DeleteMove(moviePosterDL, errFrom); // Delete prev file, if exists
                return GlobalVars.DownloadLoop(moviePosterDL, "https://image.tmdb.org/t/p/original/" + linkPoster, errFrom);
            }
            return false;
        }
    }
}
