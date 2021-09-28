using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class ImdbResult
    {
        [JsonProperty("movie_results")]
        public List<MovieResult> movie_results { get; set; }

        [JsonProperty("tv_results")]
        public List<TVResult> tv_results { get; set; }
    }

    public class MovieResult
    {
        [JsonProperty("id")]
        public string id { get; set; } = "";
    }

    public class TVResult
    {
        [JsonProperty("id")]
        public string id { get; set; } = "";
    }
}
