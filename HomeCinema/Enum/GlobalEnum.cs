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
    public enum InfoColumn
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
    public enum FileColumn
    {
        Id,
        file,
        sub,
        trailer
    }
}
