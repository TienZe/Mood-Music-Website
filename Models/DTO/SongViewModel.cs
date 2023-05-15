using PBL3.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.DTO
{
    public class ListSongItem
    {
        public Song Song { get; set; }
        public string GenreNames { get; set; }
        public string EmotionNames { get; set; }
    }
    public class CreateSongModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public string? Singer { get; set; }
        [Required]
        public string? Source { get; set; }
        public List<int>? EmotionIds { get; set; }
        public List<int>? GenreIds { get; set; }
    }
}
