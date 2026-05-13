using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;

namespace Noise.Controllers;

public class MediaController : Controller
{
    private readonly ApplicationDbContext _context;

    public MediaController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var mediaItems = await _context.MediaItems
            .Include(m => m.MusicItem!)
                .ThenInclude(m => m.Artist)
            .OrderBy(m => m.Type)
            .ThenBy(m => m.Title)
            .ToListAsync();

        return View(mediaItems);
    }

    public async Task<IActionResult> Details(int id)
    {
        var mediaItem = await _context.MediaItems
            .Include(m => m.MusicItem!)
                .ThenInclude(m => m.Artist)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (mediaItem == null)
        {
            return NotFound();
        }

        return View(mediaItem);
    }
}