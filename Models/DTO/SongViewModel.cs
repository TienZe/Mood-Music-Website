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

        [Required(ErrorMessage = "Please choose genre of this song")]
        public IEnumerable<int>? EmotionIds { get; set; }

		[Required(ErrorMessage = "Please choose emotion of this song")]
		public IEnumerable<int>? GenreIds { get; set; }
    }
    public class EditSongModel : CreateSongModel
    {
        public int SongId { get; set; }
    }
}
