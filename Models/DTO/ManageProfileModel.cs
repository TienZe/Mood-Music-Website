using Microsoft.AspNetCore.Mvc.ModelBinding;
using PBL3.Models.Domain;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PBL3.Models.DTO
{
    public enum ManagePage
    {
        ManageProfile, 
        ChangePassword
    };
    public class ManageProfileModel
    {
        public string Email { get; set; }

        [Required]
        public string? Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime? Birthday { get; set; }

        [Range(1, 3, ErrorMessage = "Please enter your gender")]
        public Gender? Gender { get; set; }
    }
}
