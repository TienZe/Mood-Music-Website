using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

namespace PBL3.Repositories.Implementation;

public class PaymentRepository : IPaymentRepository
{
    protected readonly AppDbContext context;

    public PaymentRepository(AppDbContext context)
    {
        this.context = context;
    }

    public void Add(StoredPayment payment)
    {
        payment.CreatedAt = DateTime.Now;
        context.StoredPayments.Add(payment);
        context.SaveChanges();
    }

    public void Delete(string paymentId)
    {
        StoredPayment? payment = context.StoredPayments.Find(paymentId);
        if (payment != null)
        {
            context.StoredPayments.Remove(payment);
            context.SaveChanges();
        }
    }

    public IQueryable<StoredPayment> GetAll()
    {
        return context.StoredPayments;
    }

    public StoredPayment? GetById(string paymentId)
    {
        return context.StoredPayments.Find(paymentId);
    }

    public void Update(StoredPayment payment)
    {
        context.StoredPayments.Update(payment);
		context.SaveChanges();
    }
}