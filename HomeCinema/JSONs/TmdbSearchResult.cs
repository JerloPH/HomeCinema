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
        [JsonProperty("name")]
        public string Name { get; set; } = "";
        [JsonProperty("original_name")]
        public string OrigName { get; set; } = "";
        [JsonProperty("overview")]
        public string Summary { get; set; } = "";
        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";
        [JsonProperty("release_date")]
        public string ReleaseDate { get; set; } = "";
        [JsonProperty("first_air_date")]
        public string FirstAirDate { get; set; } = "";

        [JsonProperty("genres")]
        public List<TmdbGenre> Genres { get; set; } = new List<TmdbGenre>();
        [JsonProperty("external_ids")]
        public TmdbExternalIds ExternalIds { get; set; } = new TmdbExternalIds();

        [JsonProperty("production_countries")]
        public List<TmdbProdCountry> ProdCountries { get; set; } = new List<TmdbProdCountry>();

        [JsonProperty("production_companies")]
        public List<TmdbProdCompany> ProdCompanies { get; set; } = new List<TmdbProdCompany>();
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
    public class TmdbProdCountry
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class TmdbProdCompany
    {
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
