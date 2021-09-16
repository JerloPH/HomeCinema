namespace HomeCinema
{
    public class MediaLocations
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
