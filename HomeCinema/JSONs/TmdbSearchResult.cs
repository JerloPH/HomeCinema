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
    public class TmdbMovieInfo
    {
        [JsonProperty("id")]
        public int Id{ get; set; } = 0;
        [JsonProperty("title")]
        public string Title { get; set; } = "";
        [JsonProperty("original_title")]
        public string OrigTitle { get; set; } = "";
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";
        [JsonProperty("genres")]
        public List<TmdbGenre> Genres { get; set; } = new List<TmdbGenre>();
    }

    public class TmdbExternalIds
    {
        [JsonProperty("imdb_id")]
        public string Imdb { get; set; } = "";
    }
    public class TmdbGenre
    {
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
}
