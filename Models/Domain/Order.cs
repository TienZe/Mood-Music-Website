namespace PBL3.Models.Domain
{
    public class Order
    {
        public int OrderId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Value { get; set; }
        public string Payment { get; set; } = string.Empty;
        // User thực hiện Order
        public AppUser User { get; set; } // required
        public OrderType OrderType { get; set; } // required
        public OrderStatus OrderStatus { get; set; } // required
        public Story Story { get; set; } // required
    }   
}
