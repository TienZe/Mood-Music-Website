using Microsoft.Extensions.Options;
using PayPal.Api;
using PBL3.Models.DTO;

namespace PBL3.Infrastructures;

public class PayPalService
{
    private readonly PayPalSettings _payPalSettings;
    private readonly APIContext _apiContext;

    public PayPalService(IOptions<PayPalSettings> payPalSettings)
    {
        _payPalSettings = payPalSettings.Value;

        var config = new Dictionary<string, string>
        {
            { "mode", _payPalSettings.Mode },
            { "clientId", _payPalSettings.ClientId },
            { "clientSecret", _payPalSettings.ClientSecret }
        };

        var accessToken = new OAuthTokenCredential(config).GetAccessToken();
        _apiContext = new APIContext(accessToken) { Config = config };
    }

    public Payment CreatePayment(string baseUrl, string intent, decimal amount)
    {
        var payer = new Payer { payment_method = "paypal" };

        var redirUrls = new RedirectUrls
        {
            cancel_url = $"{baseUrl}/Payment/Cancel",
            return_url = $"{baseUrl}/Payment/Success"
        };

        var paypalAmount = new Amount
        {
            currency = "USD",
            total = amount.ToString(),
        };

        var transactionList = new List<Transaction>
        {
            new Transaction
            {
                description = "Transaction description.",
                invoice_number = Convert.ToString((new Random()).Next(100000)),
                amount = paypalAmount,
            }
        };

        var payment = new Payment
        {
            intent = intent,
            payer = payer,
            transactions = transactionList,
            redirect_urls = redirUrls
        };

        return payment.Create(_apiContext);
    }

    public Payment ExecutePayment(string paymentId, string payerId)
    {
        var paymentExecution = new PaymentExecution { payer_id = payerId };
        var payment = new Payment { id = paymentId };

        return payment.Execute(_apiContext, paymentExecution);
    }
}