using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PBL3.Models.Domain;

public class StoredPayment
{
    [Key]
    public string PaymentId { get; set; }
    public string PayerId { get; set; }
    public string Token { get; set; }
    public string Status { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }

    [ForeignKey("User")]
    public string UserId { get; set; }
    public AppUser User { get; set; }
}