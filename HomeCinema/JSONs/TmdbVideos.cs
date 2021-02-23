using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class TmdbVideos
    {
        [JsonProperty("results")]
        public List<ResultVid> results { get; set; }
    }

    public class ResultVid
    {
        [JsonProperty("name")]
        public string name { get; set; } = "";
        [JsonProperty("key")]
        public string key { get; set; } = "";
        [JsonProperty("site")]
        public string site { get; set; } = "";
    }
}
