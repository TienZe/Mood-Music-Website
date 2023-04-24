namespace PBL3.Models.Domain
{
    public class Emotion
    {
        public int EmotionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Song> Songs { get; } = new();
    }
}
