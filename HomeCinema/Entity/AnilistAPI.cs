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
                  }
                  coverImage {
                    medium
                  }
                }
              }
            }";

        private static string BuildSearchPaginated(string text)
        {
            string search = text.Replace("\"", string.Empty);
            string query = "{\"query\":" + 
                "\"query {\n  Page (perPage: 20, page: 1) {\n    pageInfo {\n      total\n      currentPage\n      lastPage\n      hasNextPage\n      perPage\n    }\n    " + 
                "media (search: \\\"" + search + "\\\", type: ANIME) {\n      id\n      format\n      episodes\n      title {\n        romaji\n      }\n      coverImage {\n        medium\n      }\n    }\n  }\n}\", " +
                "\"variables\": null}";
            return query;
        }
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
                    variables = "{ search: \"" + text.Replace("\"", string.Empty) + "\"}"
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
        public static AnilistPageQuery SearchForAnime(string text, int page)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    //client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    //client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var requestContent = new StringContent(BuildSearchPaginated(text), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(Host, requestContent).Result;
                    requestContent.Dispose();
                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string contentString = response.Content.ToString();
                        var result = JsonConvert.DeserializeObject<AnilistPageQuery>(contentString);
                        response.Dispose();
                        return result;
                    }
                    return null;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
