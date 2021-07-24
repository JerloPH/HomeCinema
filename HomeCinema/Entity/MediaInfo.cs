﻿using System;
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
        }
        public string Imdb { get; set; } = "";
        public string Anilist { get; set; } = "";
        public string Title { get; set; } = "";
        public string OrigTitle { get; set; } = "";
        public string Summary { get; set; } = "";
        public string ReleaseDate { get; set; } = "";
        public string Actor { get; set; } = "";
        public string Director { get; set; } = "";
        public string Producer { get; set; } = "";
        public string Studio { get; set; } = "";
        public List<string> Country { get; set; }
        public List<string> Genre { get; set; }
        public string Trailer { get; set; } = "";
        public string PosterPath { get; set; } = "";
        public string JsonPath { get; set; } = "";
    }
}