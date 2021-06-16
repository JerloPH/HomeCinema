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
        name_ep,
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
    public enum HCFile
    {
        Id,
        file,
        sub,
        trailer
    }
}
