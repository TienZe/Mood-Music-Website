namespace PBL3.Models.Domain
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Singer { get; set; } = string.Empty;
        public string Source { get; set; } = string.Empty;
        public List<Emotion> Emotions { get; } = new();
        public List<Genre> Genres { get; } = new();
    }
}
