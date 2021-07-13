using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class TmdbPageResult
    {
        [JsonProperty("page")]
        public int page { get; set; } = 0;

        [JsonProperty("results")]
        public List<Result> results { get; set; }

    }

    public class Result
    {
        [JsonProperty("id")]
        public int id { get; set; } = 0;

        [JsonProperty("media_type")]
        public string media_type { get; set; } = "";

        [JsonProperty("title")]
        public string title { get; set; } = "";

        [JsonProperty("original_title")]
        public string original_title { get; set; } = "";

        [JsonProperty("name")]
        public string name { get; set; } = "";

        [JsonProperty("original_name")]
        public string original_name { get; set; } = "";

        [JsonProperty("overview")]
        public string overview { get; set; } = "";

        [JsonProperty("poster_path")]
        public string poster_path { get; set; } = "";
    }
}
