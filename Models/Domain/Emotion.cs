using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.Domain
{
    public class Emotion
    {
        [Required]
        public int EmotionId { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public List<Song> Songs { get; } = new();
    }
}
