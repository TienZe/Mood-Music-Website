using Microsoft.EntityFrameworkCore;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Repositories.Implementation
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }

        public IQueryable<Order> GetAllWithRelatedEntity()
        {
            return GetAll().Include(o => o.User).Include(o => o.OrderType);
        }
        public Order? GetByIdWithRelatedEntity(int id)
        {
            Order? order = GetById(id);
            if (order == null) return null;
            // Explicit load related entity
            var entityEntry = context.Entry(order);
            entityEntry.Reference(order => order.User).Load();
            entityEntry.Reference(order => order.Story).Load();
            entityEntry.Reference(order => order.OrderType).Load();
            return order;
        }
    }
}
