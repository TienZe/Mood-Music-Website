using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayPal.Api;
using PBL3.Infrastructures;
using PBL3.Models.Domain;
using PBL3.Repositories.Abstract;

[Authorize]
public class PaymentController : Controller
{
    private readonly PayPalService _payPalService;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly decimal _convertionRate;
    public PaymentController(PayPalService payPalService, IPaymentRepository paymentRepository, 
        IUserRepository userRepository ,IConfiguration configuration)
    {
        _payPalService = payPalService;
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _convertionRate = configuration.GetValue<decimal>("ConvertionRate");
    }

    public IActionResult CreatePayment()
    {
        ViewData["ConvertionRate"] = _convertionRate;
        return View("AddPoints");
    }
    public IActionResult Payment(decimal amount)
    {
        var baseUrl = $"{Request.Scheme}://{Request.Host}";
        var payment = _payPalService.CreatePayment(baseUrl, "sale", amount);

        var links = payment.links.GetEnumerator();
        string paypalRedirectUrl = null;

        while (links.MoveNext())
        {
            var link = links.Current;

            if (link.rel.ToLower().Trim().Equals("approval_url"))
            {
                paypalRedirectUrl = link.href;
            }
        }

        return Redirect(paypalRedirectUrl);
    }

    public IActionResult Success(string paymentId, string token, string payerId)
    {
        try
        {
            // Check if payment was already approved
            var storedPayment = _paymentRepository.GetById(paymentId);
            if (storedPayment is not null && storedPayment.Status == "approved")
            {
                return BadRequest();
            }

            var payment = _payPalService.ExecutePayment(paymentId, payerId);
            if (payment.state.ToLower() != "approved")
            {
                return View("Failure");
            }

            // Save payment info to database
            var userId = User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            decimal amount = Convert.ToDecimal(payment.transactions[0].amount.total);

            var newStoredPayment = new StoredPayment
            {
                PaymentId = paymentId,
                PayerId = payerId,
                Token = token,
                Status = "approved",
                Amount = amount,
                UserId = userId
            };

            _paymentRepository.Add(newStoredPayment);

            // Update user's balance
            _userRepository.IncreaseBalance(userId, amount * _convertionRate);

            return View("Success");
        }
        catch (Exception e)
        {
            return BadRequest();
        }
        
    }

    public IActionResult Cancel()
    {
        return View("Cancel");
    }
}
