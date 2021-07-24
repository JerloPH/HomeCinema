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

        // Return IMDB Id of Movie from TMDB, by searching movie title
        public static string GetIMDBId(string Movie_Title, string MOVIE_ID, string mediatype, bool showAMsg = false)
        {
            mediatype = (String.IsNullOrWhiteSpace(mediatype)) ? "movie" : mediatype;
            string ret = "";
            string errFrom = $"GlobalVars-GetIMDBId";
            // Setup vars and links
            string KEY = TMDB_KEY;
            string mediatypeUrl = mediatype.ToLower().Equals("movie") ? "movie" : "tv";
            // GET TMDB MOVIE ID
            string MovieTitle = System.Net.WebUtility.UrlEncode(Movie_Title);
            string urlJSONgetId = @"https://api.themoviedb.org/3/search/" + mediatypeUrl + "?api_key=" + KEY + "&query=" + MovieTitle;
            string JSONgetID = GlobalVars.PATH_TEMP + MOVIE_ID + "_id.json";
            string JSONgetImdb = "", MovieID = "";
            string JSONContents = "", urlJSONgetImdb;

            // Download file , force overwrite (TO GET TMDB MOVIE ID)
            if (GlobalVars.DownloadAndReplace(JSONgetID, urlJSONgetId, errFrom, showAMsg))
            {

                // Get TMDB Movie ID from JSON File (JSONgetID) downloaded
                JSONContents = GlobalVars.ReadStringFromFile(JSONgetID, errFrom);
                var objPageResult = JsonConvert.DeserializeObject<TmdbPageResult>(JSONContents);
                try { MovieID = objPageResult.results[0].id.ToString(); }
                catch { MovieID = ""; }
                //ShowInfo("Movie ID: " + MovieID);

                // Check if MovieID is not empty
                if (String.IsNullOrWhiteSpace(MovieID) == false)
                {
                    // GET IMDB
                    urlJSONgetImdb = @"https://api.themoviedb.org/3/" + mediatype + "/" + MovieID + "?api_key=" + KEY;
                    urlJSONgetImdb += (mediatype != "movie") ? "&append_to_response=external_ids" : ""; // Append external_ids param for non-movie
                    JSONgetImdb = GlobalVars.PATH_TEMP + MOVIE_ID + "_imdb.json";

                    // Download file if not existing (TO GET IMDB Id)
                    if (GlobalVars.DownloadAndReplace(JSONgetImdb, urlJSONgetImdb, errFrom, showAMsg))
                    {
                        JSONContents = GlobalVars.ReadStringFromFile(JSONgetImdb, errFrom);
                        try
                        {
                            var objMovieInfo = JsonConvert.DeserializeObject<MovieInfo>(JSONContents);
                            ret = (mediatype == "movie") ? objMovieInfo.imdb_id : objMovieInfo.external_ids.imdb_id;
                        }
                        catch { ret = ""; }
                    }
                }
            }
            // Delete files if MOVIE_ID = dummy
            if (MOVIE_ID == "dummy")
            {
                // Clean up
                GlobalVars.TryDelete(JSONgetID, errFrom);
                GlobalVars.TryDelete(JSONgetImdb, errFrom);
            }
            return ret;
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
        // Return MediaInfo object with info on Movie, based on IMDB Id
        public static MediaInfo GetMovieInfoByImdb(string IMDB_ID, string mediatype, bool showAMsg = false)
        {
            // Setup vars
            string errFrom = "GlobalVars-GetMovieInfoByImdb";
            string mediaTypeUrl = mediatype.Equals("movie") ? "movie" : "tv";
            var MediaInfo = new MediaInfo();
            string TMDB_MovieID = "";
            // File paths
            string JSONmovieinfo = GlobalVars.PATH_TEMP + IMDB_ID + ".json";
            string JSONfindmovie = GlobalVars.PATH_TEMP + IMDB_ID + "_info.json";
            string JSONcrewcast = GlobalVars.PATH_TEMP + IMDB_ID + "_crewcast.json";
            string JSONfindtrailer = GlobalVars.PATH_TEMP + IMDB_ID + "_videos.json";
            // Links
            string urlJSONMovieInfo, urlJSONFindMovie, urlJSONcrewcast, urlJSONtrailer;

            string JSONContents;

            // If JSON File DOES not exists, Download it
            urlJSONMovieInfo = @"https://api.themoviedb.org/3/find/" + $"{IMDB_ID}?api_key={TMDB_KEY}&language=en-US&external_source=imdb_id";
            // JSON - MOVIE WITH GIVEN IMDB
            GlobalVars.DownloadAndReplace(JSONmovieinfo, urlJSONMovieInfo, errFrom + " [JSONmovieinfo]", showAMsg);
            if (File.Exists(JSONmovieinfo))
            {
                try
                {
                    JSONContents = GlobalVars.ReadStringFromFile(JSONmovieinfo, errFrom);
                    var objJson = JsonConvert.DeserializeObject<ImdbResult>(JSONContents);
                    TMDB_MovieID = mediatype.Equals("movie") ? objJson.movie_results[0].id : objJson.tv_results[0].id;
                }
                catch { TMDB_MovieID = ""; }
            }

            // Find main movie info
            if (!String.IsNullOrWhiteSpace(TMDB_MovieID))
            {
                // JSON - FIND USING IMDB
                urlJSONFindMovie = @"https://api.themoviedb.org/3/" + mediaTypeUrl + "/" + TMDB_MovieID + "?api_key=" + TMDB_KEY;
                // FETCH TRAILER
                urlJSONtrailer = @"https://api.themoviedb.org/3/" + mediaTypeUrl + "/" + TMDB_MovieID + "/videos?api_key=" + TMDB_KEY;

                // Download file, if not existing, to fetch info
                GlobalVars.DownloadAndReplace(JSONfindmovie, urlJSONFindMovie, errFrom, showAMsg);

                // Download file, if not existing, to fetch trailer
                GlobalVars.DownloadAndReplace(JSONfindtrailer, urlJSONtrailer, errFrom, showAMsg);

                // Get contents from JSON File, Deserialize it into MovieInfo class
                if (File.Exists(JSONfindmovie))
                {
                    // Save to list  the json file  path
                    MediaInfo.JsonPath = JSONfindmovie;

                    // Get contents of JSON file
                    string contents = GlobalVars.ReadStringFromFile(JSONfindmovie, errFrom + " [JSONfindmovie]");

                    // Deserialize JSON and Parse contents
                    MovieInfo movie = JsonConvert.DeserializeObject<MovieInfo>(contents);
                    if (movie != null)
                    {
                        if (mediatype == "movie")
                        {
                            MediaInfo.Title = movie.title; // title
                            MediaInfo.OrigTitle = movie.original_title; // original title
                            MediaInfo.ReleaseDate = movie.release_date; // release date
                        }
                        else
                        {
                            MediaInfo.Title = movie.name; // series title
                            MediaInfo.OrigTitle = movie.original_name;
                            MediaInfo.ReleaseDate = movie.first_air_date; // release date
                        }
                        MediaInfo.Summary = movie.overview; // summary / overview
                        MediaInfo.PosterPath = (!String.IsNullOrWhiteSpace(movie.poster_path)) ? movie.poster_path : String.Empty; // poster_path
                        // Country
                        foreach (ProdCountry c in movie.production_countries)
                        {
                            MediaInfo.Country.Add(c.name.Trim());
                        }
                        // Get genres
                        if (contents.Contains("genres"))
                        {
                            var jObj = (JObject)JsonConvert.DeserializeObject(contents);
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
                                    MediaInfo.Genre.Add(valTrim);
                                }
                            }
                        }
                        // Studio
                        try { MediaInfo.Studio = movie.production_companies.Select(a => a.name).ToList().Aggregate((b, c) => b + ", " + c); }
                        catch { MediaInfo.Studio = ""; }
                    }
                }

                // Get Trailer
                if (File.Exists(JSONfindtrailer))
                {
                    // Get contents of JSON file
                    string contents = GlobalVars.ReadStringFromFile(JSONfindtrailer, errFrom + " [JSONfindtrailer]");
                    try
                    {
                        var objJson = JsonConvert.DeserializeObject<TmdbVideos>(contents);
                        if (objJson.results[0].site.ToLower() == "youtube")
                        {
                            MediaInfo.Trailer = objJson.results[0].key;
                        }
                    }
                    catch
                    {
                        MediaInfo.Trailer = "";
                    }
                }

                // Get crew and cast
                urlJSONcrewcast = @"https://api.themoviedb.org/3/" + mediaTypeUrl + "/" + TMDB_MovieID + "/credits?api_key=" + TMDB_KEY;
                GlobalVars.DownloadAndReplace(JSONcrewcast, urlJSONcrewcast, errFrom + " [JSONcrewcast]", showAMsg);
                if (File.Exists(JSONcrewcast))
                {
                    // Unparse json into object list
                    string contents = GlobalVars.ReadStringFromFile(JSONcrewcast, errFrom + " [JSONcrewcast]");
                    CastCrew castcrew = JsonConvert.DeserializeObject<CastCrew>(contents);

                    string tmp = ""; // temporary string var. Use to get strings
                    foreach (Cast c in castcrew.cast)
                    {
                        // Get informations
                        tmp += c.name + ", ";
                    }
                    // Save to list as artist
                    MediaInfo.Actor = tmp.TrimEnd().TrimEnd(',');

                    // get Director and producer
                    string tmpDir = "", tmpProd = ""; // Use to get director and producer
                    foreach (Crew c in castcrew.crew)
                    {
                        // Get informations
                        if (c.job == "Director")
                        {
                            tmpDir += c.name + ", ";
                        }
                        else if (c.job == "Producer")
                        {
                            tmpProd += c.name + ", ";
                        }
                    }
                    // Save to list as director and producer
                    MediaInfo.Director = tmpDir.TrimEnd().TrimEnd(',');
                    MediaInfo.Producer = tmpProd.TrimEnd().TrimEnd(',');
                }
            }
            return MediaInfo;
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
