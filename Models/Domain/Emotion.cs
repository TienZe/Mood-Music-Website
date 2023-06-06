using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.Domain
{
    public class Emotion
    {
        public int EmotionId { get; set; }

        [Required(ErrorMessage = "Please enter the name of genre")]
        public string Name { get; set; } = string.Empty;

        [BindNever]
        public List<Song> Songs { get; } = new();
    }
}
