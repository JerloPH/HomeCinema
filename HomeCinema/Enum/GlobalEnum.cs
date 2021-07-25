using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public enum HCIcons
    {
        Loading = 0,
        Check = 1,
        Warning = 2
    }
    public enum HCInfo
    {
        Id,
        imdb,
        name,
        name_orig,
        name_series,
        season,
        episode,
        country,
        category,
        genre,
        studio,
        producer,
        director,
        artist,
        year,
        summary
    }
    public static class HCFile
    {
        public static string Id = "Id";
        public static string File = "file";
        public static string Sub = "sub";
        public static string Trailer = "trailer";
    }
}
