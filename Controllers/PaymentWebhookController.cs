using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WillMyWay.Data;
using WillMyWay.Models;

[Route("api/[controller]")]
[ApiController]
public class PaymentWebhookController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PaymentWebhookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost("stripe")]
    public async Task<IActionResult> StripeWebhook([FromBody] JObject payload)
    {
        string eventType = payload["type"]?.ToString();

        if (eventType == "checkout.session.completed")
        {
            string userEmail = payload["data"]["object"]["customer_email"]?.ToString();
            string audioIdStr = payload["data"]["object"]["metadata"]["audioId"]?.ToString();

            if (int.TryParse(audioIdStr, out int audioId))
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    _context.Purchases.Add(new Purchase
                    {
                        UserId = user.Id,
                        AudioId = audioId
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }

    [HttpPost("paypal")]
    public async Task<IActionResult> PayPalWebhook([FromBody] JObject payload)
    {
        string eventType = payload["event_type"]?.ToString();

        if (eventType == "CHECKOUT.ORDER.APPROVED")
        {
            string userEmail = payload["resource"]["payer"]["email_address"]?.ToString();
            string audioIdStr = payload["resource"]["purchase_units"][0]["custom_id"]?.ToString();

            if (int.TryParse(audioIdStr, out int audioId))
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    _context.Purchases.Add(new Purchase
                    {
                        UserId = user.Id,
                        AudioId = audioId
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }

    [HttpPost("nowpayments")]
    public async Task<IActionResult> NowPaymentsWebhook([FromBody] JObject payload)
    {
        string paymentStatus = payload["payment_status"]?.ToString();

        if (paymentStatus == "finished")
        {
            string userEmail = payload["order_description"]?.ToString();
            string audioIdStr = payload["order_id"]?.ToString();

            if (int.TryParse(audioIdStr, out int audioId))
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == userEmail);
                if (user != null)
                {
                    _context.Purchases.Add(new Purchase
                    {
                        UserId = user.Id,
                        AudioId = audioId
                    });
                    await _context.SaveChangesAsync();
                }
            }
        }

        return Ok();
    }
}