using Newtonsoft.Json;
using System.Collections.Generic;

namespace HomeCinema
{
    public class AnilistPageQuery
    {
        [JsonProperty("data")]
        public AnilistDataPaginated Data { get; set; }
    }
    public class AnilistSingleQuery
    {
        [JsonProperty("data")]
        public AnilistDataSingle Data { get; set; }
    }
    public class AnilistDataPaginated
    {
        [JsonProperty("Page")]
        public AnilistPage Page { get; set; }
    }
    public class AnilistDataSingle
    {
        [JsonProperty("Media")]
        public AnilistMediaInfo Media { get; set; }
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
        [JsonProperty("format")]
        public string Format { get; set; } = "";
        [JsonProperty("episodes")]
        public int Episodes { get; set; } = 0;
        [JsonProperty("title")]
        public AnilistMediaTitle Title { get; set; }
        [JsonProperty("coverImage")]
        public AnilistMediaCoverImage CoverImage { get; set; }
    }
    public class AnilistMediaTitle
    {
        [JsonProperty("romaji")]
        public string Romaji { get; set; } = "";
        [JsonProperty("english")]
        public string English { get; set; } = "";
    }
    public class AnilistMediaCoverImage
    {
        [JsonProperty("medium")]
        public string Medium { get; set; } = "";
        [JsonProperty("large")]
        public string Large { get; set; } = "";
    }
    public class AnilistMediaDate
    {
        [JsonProperty("year")]
        public int Year { get; set; } = 0;
        [JsonProperty("month")]
        public int Month { get; set; } = 0;
        [JsonProperty("day")]
        public int Day { get; set; } = 0;
    }
    public class AnilistMediaTrailer
    {
        [JsonProperty("id")]
        public string Id { get; set; } = "";
        [JsonProperty("site")]
        public string Site { get; set; } = "";
    }
    public class AnilistMediaStudios
    {
        [JsonProperty("edges")]
        public List<AnilistMediaStudiosEdge> Edge { get; set; } = new List<AnilistMediaStudiosEdge>();
    }
    public class AnilistMediaStudiosEdge
    {
        [JsonProperty("isMain")]
        public bool IsMain { get; set; } = false;
        [JsonProperty("node")]
        public AnilistMediaStudiosEdgeNode Node { get; set; } = new AnilistMediaStudiosEdgeNode();
    }
    public class AnilistMediaStudiosEdgeNode
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;
        [JsonProperty("name")]
        public string Name { get; set; } = "";
    }
    public class AnilistMediaStaff
    {
        [JsonProperty("edges")]
        public List<AnilistMediaStaffEdge> Edge { get; set; } = new List<AnilistMediaStaffEdge>();
    }
    public class AnilistMediaStaffEdge
    {
        [JsonProperty("role")]
        public string Role { get; set; } = "";
        [JsonProperty("node")]
        public AnilistMediaStaffEdgeNode Node { get; set; } = new AnilistMediaStaffEdgeNode();
    }
    public class AnilistMediaStaffEdgeNode
    {
        [JsonProperty("name")]
        public AnilistMediaStaffName Name { get; set; } = new AnilistMediaStaffName();
    }
    public class AnilistMediaStaffName
    {
        [JsonProperty("full")]
        public string Full { get; set; } = "";
    }
    public class AnilistMediaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;
        [JsonProperty("format")]
        public string Format { get; set; } = "";
        [JsonProperty("episodes")]
        public int Episodes { get; set; } = 0;
        [JsonProperty("countryOfOrigin")]
        public string CountryOfOrigin { get; set; } = "";
        [JsonProperty("description")]
        public string Description { get; set; } = "";
        
        [JsonProperty("genres")]
        public List<string> Genres{ get; set; } = new List<string>();

        [JsonProperty("staff")]
        public AnilistMediaStaff Staff { get; set; } = new AnilistMediaStaff();
        [JsonProperty("studios")]
        public AnilistMediaStudios Studios { get; set; } = new AnilistMediaStudios();
        [JsonProperty("startDate")]
        public AnilistMediaDate StartDate { get; set; } = new AnilistMediaDate();
        [JsonProperty("trailer")]
        public AnilistMediaTrailer Trailer { get; set; } = new AnilistMediaTrailer();
        [JsonProperty("title")]
        public AnilistMediaTitle Title { get; set; } = new AnilistMediaTitle();
        [JsonProperty("coverImage")]
        public AnilistMediaCoverImage CoverImage { get; set; } = new AnilistMediaCoverImage();
        
    }
}
