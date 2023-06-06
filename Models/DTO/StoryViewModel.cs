using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.DTO
{
    public class CreateStoryModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        [Display(Name = "One-time using price")]
        public decimal OneTimePrice { get; set; }
        [Required]
        [Display(Name = "Lifetime using price")]
        public decimal LifeTimePrice { get; set; }
        [Required]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Please upload avatar of this story")]
        public IFormFile? AvatarImage { get; set; }
        [Required(ErrorMessage = "Please upload audio file")]
        public IFormFile? Audio { get; set; }
    }
    public class EditStoryModel
    {
        [Required]
        public int StoryId { get; set; }
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        [Display(Name = "One-time using price")]
        public decimal OneTimePrice { get; set; }
        [Required]
        [Display(Name = "Lifetime using price")]
        public decimal LifeTimePrice { get; set; }
        [Required]
        public string? Description { get; set; }
        public string? AudioFileName { get; set; }
        public string? ImageFileName { get; set; }

        // input upload audio mới
        public IFormFile? NewAudio { get; set; }
        // input upload image mới
        public IFormFile? NewImage { get; set; }
    }
}
