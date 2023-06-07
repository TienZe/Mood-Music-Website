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
    }
}
