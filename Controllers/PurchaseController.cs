using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WillMyWay.Data;
using WillMyWay.Models;

[Route("api/[controller]")]
[ApiController]
public class PurchaseController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PurchaseController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> Purchase(Purchase purchase)
    {
        _context.Purchases.Add(purchase);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpGet("{userId}")]
    public async Task<IActionResult> GetUserPurchases(int userId)
    {
        var purchases = await _context.Purchases
            .Where(p => p.UserId == userId)
            .Include(p => p.Audio)
            .ToListAsync();

        return Ok(purchases.Select(p => p.Audio));
    }
}