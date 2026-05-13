using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;

namespace Noise.Controllers;

public class ArtistsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ArtistsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var artists = await _context.Artists
            .Include(a => a.Country)
            .Include(a => a.Genre)
            .OrderBy(a => a.Name)
            .ToListAsync();

        return View(artists);
    }

    public async Task<IActionResult> Details(int id)
    {
        var artist = await _context.Artists
            .Include(a => a.Country)
            .Include(a => a.Genre)
            .Include(a => a.MusicItems)
            .FirstOrDefaultAsync(a => a.Id == id);

        if (artist == null)
        {
            return NotFound();
        }

        return View(artist);
    }
}