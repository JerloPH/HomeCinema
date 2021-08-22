using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public class MediaInfo
    {
        public MediaInfo()
        {
            Imdb = "";
            Country = new List<string>();
            Genre = new List<string>();
            Studio = new List<string>();
            Director = new List<string>();
            Producer = new List<string>();
            Actor = new List<string>();
        }
        public void Dispose()
        {
            Id = "";
            Imdb = "";
            Anilist = "";
            Title = "";
            OrigTitle = "";
            SeriesName = "";
            Summary = "";
            ReleaseDate = "";
            Trailer = "";
            PosterPath = "";
            FilePath = "";
            FileSub = "";
            Country.Clear();
            Genre.Clear();
            Studio.Clear();
            Director.Clear();
            Producer.Clear();
            Actor.Clear();
        }
        public string Id { get; set; } = "";
        public int Category { get; set; } = 0;
        public string Imdb { get; set; } = "";
        public string Anilist { get; set; } = "";
        public string Title { get; set; } = "";
        public string OrigTitle { get; set; } = "";
        public string SeriesName { get; set; } = "";
        public string Summary { get; set; } = "";
        public string ReleaseDate { get; set; } = "";
        public List<string> Actor { get; set; }
        public List<string> Director { get; set; }
        public List<string> Producer { get; set; }
        public List<string> Studio { get; set; }
        public List<string> Country { get; set; }
        public List<string> Genre { get; set; }
        public string Trailer { get; set; } = "";
        public string PosterPath { get; set; } = "";
        public int Seasons { get; set; } = 0;
        public int Episodes { get; set; } = 0;
        public string FilePath { get; set; } = "";
        public string FileSub { get; set; } = "";
    }
}
