using PBL3.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.DTO
{
    public class CreateSongModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Artist { get; set; }
		[Required(ErrorMessage = "Please upload audio file")]
		public IFormFile? Audio { get; set; }

        [Required(ErrorMessage = "Please choose emotion of this song")]
        public IEnumerable<int>? EmotionIds { get; set; }

		[Required(ErrorMessage = "Please choose genre of this song")]
		public IEnumerable<int>? GenreIds { get; set; }
    }
    public class EditSongModel
    {
        public int SongId { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Artist { get; set; }

		[Required(ErrorMessage = "Please choose emotion of this song")]
		public IEnumerable<int>? EmotionIds { get; set; }

		[Required(ErrorMessage = "Please choose genre of this song")]
		public IEnumerable<int>? GenreIds { get; set; }

		public string? CurrentAudio { get; set; }
        public IFormFile? NewAudio { get; set; }
    }
}
