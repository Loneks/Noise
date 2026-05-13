using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;

namespace Noise.Controllers;

public class NewsController : Controller
{
    private readonly ApplicationDbContext _context;

    public NewsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var news = await _context.News
            .OrderByDescending(n => n.PublishedAt)
            .ToListAsync();

        return View(news);
    }

    public async Task<IActionResult> Details(int id)
    {
        var newsItem = await _context.News
            .FirstOrDefaultAsync(n => n.Id == id);

        if (newsItem == null)
        {
            return NotFound();
        }

        return View(newsItem);
    }
}