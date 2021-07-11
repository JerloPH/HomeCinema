using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeCinema
{
    public struct MediaLocations
    {
        public MediaLocations(string path, string mediatype, string src)
        {
            Path = path;
            MediaType = mediatype;
            Source = src;
        }

        public string Path { get; }
        public string MediaType { get; }
        public string Source { get; }
    }
}
