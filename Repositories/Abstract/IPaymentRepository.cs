using PBL3.Models.Domain;

namespace PBL3.Repositories.Abstract;

public interface IPaymentRepository
{
    StoredPayment? GetById(string paymentId);
    IQueryable<StoredPayment> GetAll();
    void Add(StoredPayment payment);
    void Update(StoredPayment payment);
    void Delete(string paymentId);
}