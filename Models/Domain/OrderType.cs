namespace PBL3.Models.Domain
{
    public class OrderType
    {
        public int OrderTypeId { get; set; }
        public string Type { get; set; } = string.Empty;
        // Danh sách các Order có OrderType tương ứng
        public List<Order> Orders { get; } = new();
    }
}
