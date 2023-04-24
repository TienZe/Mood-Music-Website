using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace PBL3.Models.Domain
{
    public enum Gender { Male = 1, Female = 2, Optional = 3 };
    public class AppUser : IdentityUser
    {
        [StringLength(200)]
        public string? Name { get; set; }
        [Column(TypeName = "date")]
        public DateTime? Birthday { get; set; }

        [Range(1, 3, ErrorMessage = "Please enter your gender")]
        public Gender? Gender { get; set; }
        // Các Order được thực hiện bởi User
        public List<Order> Orders { get; } = new();
        // Danh sách story mà ng dùng đã mua
        public List<Story> Stories { get; } = new();
    }
}
