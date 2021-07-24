using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeCinema
{
    public static class AnilistAPI
    {
        private static string Host = "https://graphql.anilist.co";
        
        private static string BuildSearchPaginated(string text)
        {
            string search = text.Replace("\"", string.Empty);
            string query = "{\"query\":" + 
                "\"query {\n  Page (perPage: 20, page: 1) {\n    pageInfo {\n      total\n      currentPage\n      lastPage\n      hasNextPage\n      perPage\n    }\n    " + 
                "media (search: \\\"" + search + "\\\", type: ANIME) {\n      id\n      format\n      episodes\n      title {\n        romaji\n      }\n      coverImage {\n        medium\n      }\n    }\n  }\n}\", " +
                "\"variables\": null}";
            /*
            string query = "{\"query\":\"" + @"query {
              Page (page: 1, perPage: 20)
              {
                pageInfo {
                  total
                  currentPage
                  lastPage
                  hasNextPage
                  perPage
                }
                media (type: ANIME, search: " + $"\"{search}\"" + @") {
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
            query += "}\",\"variables\":null}";
            */
            return query;
        }

        public static AnilistPageQuery SearchForAnime(string text)
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
