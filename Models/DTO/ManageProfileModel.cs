using Microsoft.AspNetCore.Mvc.ModelBinding;
using PBL3.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.DTO
{
    public class ManageProfileModel
    {
        public string Account { get; set; } // luu bang Email

        [Required]
        public string? Name { get; set; } // User name

		[DataType(DataType.Password)]
		public string? NewPassword { get; set; }

		[DataType(DataType.Password)]
		[Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmPassword { get; set; }

        [Phone(ErrorMessage = "Please enter a valid phone number")]
        [Required]
        public string? PhoneNumber { get; set; }

        public int? Age { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Required]
        public Gender? Gender { get; set; }
    }
}
