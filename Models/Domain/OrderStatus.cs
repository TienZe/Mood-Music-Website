namespace PBL3.Models.Domain
{
    public class OrderStatus
    {
        public int OrderStatusId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        // Danh sách các Order có OrderStatus tương ứng
        public List<Order> Orders { get; } = new();
    }
}
