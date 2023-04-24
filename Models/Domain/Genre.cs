namespace PBL3.Models.Domain
{
    public class Genre
    {
        public int GenreId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Song> Songs { get; } = new();
    }
}
