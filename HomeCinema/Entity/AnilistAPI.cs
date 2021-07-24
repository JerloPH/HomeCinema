using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeCinema.Entity
{
    public static class AnilistAPI
    {
        private static string Host = "https://graphql.anilist.co";
        
        private static string BuildSearchPaginated(string text)
        {
            string search = text.Replace("\"", string.Empty);
            string query = @"query {
              Page (page: 1, perPage: 20)
              {
                pageInfo {
                  total
                  currentPage
                  lastPage
                  hasNextPage
                  perPage
                }
                media (type: ANIME, search: " + search + @") {
                  id
                  title {
                    romaji
                  }
                  coverImage {
                    medium
                  }
                }
              }
            }";
            return query;
        }

        public async static Task<AnilistPageQuery> SearchForAnime(string text)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Content-Type", "application/json");
                    client.DefaultRequestHeaders.Add("Accept", "application/json");

                    var requestContent = new StringContent(BuildSearchPaginated(text), Encoding.UTF8, "application/json");
                    HttpResponseMessage response = await client.PostAsync(Host, requestContent);
                    requestContent.Dispose();

                    string contentString = await response.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<AnilistPageQuery>(contentString);
                    response.Dispose();
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }
    }
}
