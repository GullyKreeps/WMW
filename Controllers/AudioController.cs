using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WillMyWay.Data;
using WillMyWay.Models;

[Route("api/[controller]")]
[ApiController]
public class AudioController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public AudioController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var audios = await _context.Audios.ToListAsync();
        return Ok(audios);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Audio audio)
    {
        _context.Audios.Add(audio);
        await _context.SaveChangesAsync();
        return Ok(audio);
    }

    [HttpPut("{id}/price")]
    public async Task<IActionResult> UpdatePrice(int id, decimal price)
    {
        var audio = await _context.Audios.FindAsync(id);
        if (audio == null) return NotFound();
        audio.Price = price;
        await _context.SaveChangesAsync();
        return Ok(audio);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var audio = await _context.Audios.FindAsync(id);
        if (audio == null) return NotFound();
        _context.Audios.Remove(audio);
        await _context.SaveChangesAsync();
        return Ok();
    }
}