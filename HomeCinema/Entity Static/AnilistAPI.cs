using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace HomeCinema
{
    public static class AnilistAPI
    {
        private static string Host = "https://graphql.anilist.co";
        private static string Query = @"query ($search: String) {
              Page (page: 1, perPage: 25)
              {
                pageInfo {
                  total
                  currentPage
                  lastPage
                  hasNextPage
                  perPage
                }
                media (type: ANIME, search: $search) {
                  id
                  format
                  episodes
                  title {
                    romaji
                    english
                  }
                  coverImage {
                    medium
                  }
                }
              }
            }";
        private static string QueryWithId = @"query ($Id: Int) {";

        public static AnilistPageQuery SearchForAnime(string text)
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

                var result = client.Execute(request).Content;
                var jsonObj = JsonConvert.DeserializeObject<AnilistPageQuery>(result);
                return jsonObj;
            }
            catch
            {
                return null;
            }
        }
        public static MediaInfo GetMovieInfoFromAnilist(string Id, string mediaType)
        {
            return new MediaInfo();
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
