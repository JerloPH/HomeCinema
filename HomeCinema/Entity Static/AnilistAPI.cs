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
    }
}
