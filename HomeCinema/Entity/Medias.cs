namespace HomeCinema
{
    public class Medias
    {
        public Medias(string filepath, string mediatype, string src, string rootfolder)
        {
            FilePath = filepath;
            MediaType = mediatype;
            Source = src;
            RootFolder = rootfolder;
        }

        public string FilePath { get; }
        public string MediaType { get; }
        public string Source { get; }
        public string RootFolder { get; set; }
    }
}
