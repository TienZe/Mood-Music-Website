using PBL3.Models.Domain;

namespace PBL3.Models.DTO
{
    public class OrderViewModel
    {
        public string? OrderName { get ; set; }
        public string? User { get; set; }
        public string? UserAccount { get; set; }
        public string? Day { get; set; }
        public string? OrderType { get; set; } 
        public decimal Price { get; set; }
        public string? StoryImage { get; set; }
    }
}
