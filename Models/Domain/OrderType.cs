namespace PBL3.Models.Domain
{
    public class OrderType
    {
        public enum Type { Onetime = 1, Lifetime }
        public int OrderTypeId { get; set; }
        public Type Name { get; set; }
        // Danh sách các Order có OrderType tương ứng
        public List<Order> Orders { get; } = new();
    }
}
