using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class AnilistPageQuery
    {
        [JsonProperty("data")]
        public AnilistDataPaginated Data { get; set; }
    }
    public class AnilistDataPaginated
    {
        [JsonProperty("Page")]
        public AnilistPage Page { get; set; }
    }
    public class AnilistPage
    {
        [JsonProperty("pageInfo")]
        public AnilistPageInfo PageInfo { get; set; }
        [JsonProperty("media")]
        public List<AnilistMediaPageResult> MediaList { get; set; }
    }
    public class AnilistPageInfo
    {
        [JsonProperty("total")]
        public int Total { get; set; } = 0;
        [JsonProperty("currentPage")]
        public int CurrentPage { get; set; } = 0;
        [JsonProperty("lastPage")]
        public int LastPage { get; set; } = 0;
        [JsonProperty("perPage")]
        public int PerPage { get; set; } = 0;
        [JsonProperty("hasNextPage")]
        public bool HasNextPage { get; set; } = false;
    }
    public class AnilistMediaPageResult
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;
        [JsonProperty("title")]
        public AnilistMediaTitle Title { get; set; }
        [JsonProperty("coverImage")]
        public AnilistMediaCoverImage CoverImage { get; set; }
    }
    public class AnilistMediaTitle
    {
        [JsonProperty("romaji")]
        public string Romaji { get; set; } = "";
    }
    public class AnilistMediaCoverImage
    {
        [JsonProperty("medium")]
        public string Medium { get; set; } = "";
    }
}
