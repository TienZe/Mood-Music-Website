using PBL3.Models.Domain;

namespace PBL3.Repositories.Abstract
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetAllWithRelatedEntity();
        Order? GetByIdWithRelatedEntity(int id);
    }
}
