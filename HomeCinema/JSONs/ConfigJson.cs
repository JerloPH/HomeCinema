using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HomeCinema
{
    public class ConfigJson
    {
        // Config for TMDB API
        [JsonProperty("TmdbApiKey")]
        public string TmdbApiKey { get; set; } = "";

        // Anilist API
        [JsonProperty("AnilistClientId")]
        public string AnilistClientId { get; set; } = "";
        [JsonProperty("AnilistClientSecret")]
        public string AnilistClientSecret { get; set; } = "";
    }
}
