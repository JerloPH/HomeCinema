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
    public class TmdbPageResult
    {
        [JsonProperty("results")]
        public List<TmdbResult> results { get; set; }

    }
    public class TmdbSearchFromImdb
    {
        [JsonProperty("movie_results")]
        public List<TmdbResult> MovieResults { get; set; } = new List<TmdbResult>();

        [JsonProperty("tv_results")]
        public List<TmdbResult> TVResults { get; set; } = new List<TmdbResult>();
    }
    public class TmdbResult
    {
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [JsonProperty("media_type")]
        public string MediaType { get; set; } = "";

        [JsonProperty("name")]
        public string Name { get; set; } = "";

        [JsonProperty("title")]
        public string Title { get; set; } = "";

        [JsonProperty("poster_path")]
        public string PosterPath { get; set; } = "";
    }
    public class TmdbSearchMovieInfo
    {
        [JsonProperty("imdb_id")]
        public string imdb_id { get; set; } = "";

        [JsonProperty("external_ids")]
        public TmdbExternalIds external_ids { get; set; }
    }
    public class TmdbMovieInfo
    {
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

        [JsonProperty("number_of_episodes")]
        public int EpisodesCount { get; set; } = 0;

        [JsonProperty("number_of_seasons")]
        public int SeasonsCount { get; set; } = 0;

        [JsonProperty("genres")]
        public List<TmdbGenre> Genres { get; set; } = new List<TmdbGenre>();

        [JsonProperty("external_ids")]
        public TmdbExternalIds ExternalIds { get; set; } = new TmdbExternalIds();

        [JsonProperty("production_countries")]
        public List<TmdbProdCountry> ProdCountries { get; set; } = new List<TmdbProdCountry>();

        [JsonProperty("production_companies")]
        public List<TmdbProdCompany> ProdCompanies { get; set; } = new List<TmdbProdCompany>();

        [JsonProperty("videos")]
        public TmdbVideos Videos { get; set; } = new TmdbVideos();

        [JsonProperty("credits")]
        public TmdbCastCrew CastCrew { get; set; } = new TmdbCastCrew();
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

    public class TmdbCastCrew
    {
        [JsonProperty("cast")]
        public List<TmdbCast> cast { get; set; } = new List<TmdbCast>();

        [JsonProperty("crew")]
        public List<TmdbCrew> crew { get; set; } = new List<TmdbCrew>();
    }

    public class TmdbCast
    {
        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class TmdbCrew
    {
        [JsonProperty("job")]
        public string job { get; set; }

        [JsonProperty("name")]
        public string name { get; set; }
    }

    public class TmdbVideos
    {
        [JsonProperty("results")]
        public List<TmdbResultVid> results { get; set; }
    }

    public class TmdbResultVid
    {
        [JsonProperty("key")]
        public string key { get; set; } = "";
        [JsonProperty("site")]
        public string site { get; set; } = "";
    }
}
