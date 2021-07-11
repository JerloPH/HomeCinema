using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public struct Medias
    {
        public Medias(string filepath, string mediatype, string src)
        {
            FilePath = filepath;
            MediaType = mediatype;
            Source = src;
        }

        public string FilePath { get; }
        public string MediaType { get; }
        public string Source { get; }
    }
}
