using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class TmdbSearchResult
    {
        [JsonProperty("total_results")]
        public int TotalResults { get; set; } = 0;

        [JsonProperty("results")]
        public List<TmdbResult> Results { get; set; } = new List<TmdbResult>();
    }
    public class TmdbResult
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("original_name")]
        public string OrigName { get; set; } = "";
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";
    }
}
