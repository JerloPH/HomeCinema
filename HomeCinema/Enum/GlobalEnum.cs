using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public enum HCIcons
    {
        None = 0,
        Loading = 1,
        Check = 2,
        Info = 3,
        Warning = 4,
        Error = 5,
        Question = 6
    }
    public static class HCSource
    {
        public static string[] sources = new string[] { "TMDB", "Anilist" };
        public static string tmdb = "tmdb";
        public static string anilist = "anilist";
    }
    public static class HCTable
    {
        public static string info = "info";
        public static string filepath = "filepath";
    }
    public static class HCInfo
    {
        public static string Id = "UId";
        public static string imdb = "imdb";
        public static string anilist = "anilist";
        public static string name = "name";
        public static string name_orig = "name_orig";
        public static string name_series = "name_series";
        public static string season = "season";
        public static string episode = "episode";
        public static string country = "country";
        public static string category = "category";
        public static string genre = "genre";
        public static string studio = "studio";
        public static string producer = "producer";
        public static string director = "director";
        public static string artist = "artist";
        public static string year = "year";
        public static string summary = "summary";
    }
    public static class HCFile
    {
        public static string Id = "UId";
        public static string File = "file";
        public static string Root = "rootFolder";
        public static string Sub = "sub";
        public static string Trailer = "trailer";
    }
}
