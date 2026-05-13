using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;

namespace Noise.Controllers;

public class GenresController : Controller
{
    private readonly ApplicationDbContext _context;

    public GenresController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var genres = await _context.Genres
            .Include(g => g.Country)
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(genres);
    }

    public async Task<IActionResult> Details(int id)
    {
        var genre = await _context.Genres
            .Include(g => g.Country)
            .Include(g => g.Artists)
            .Include(g => g.MusicItems)
            .FirstOrDefaultAsync(g => g.Id == id);

        if (genre == null)
        {
            return NotFound();
        }

        return View(genre);
    }
}