using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;
using Noise.ViewModels;

namespace Noise.Controllers;

public class SearchController : Controller
{
    private readonly ApplicationDbContext _context;

    public SearchController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? query)
    {
        ViewData["Title"] = "Search";
        ViewData["SectionName"] = "Search";

        query = query?.Trim();

        if (string.IsNullOrWhiteSpace(query))
        {
            ViewData["Query"] = "";
            return View(new SearchResultViewModel());
        }

        var genres = await _context.Genres
            .Include(g => g.Country)
            .Where(g =>
                g.Name.Contains(query) ||
                g.Description.Contains(query))
            .ToListAsync();

        var artists = await _context.Artists
            .Include(a => a.Country)
            .Include(a => a.Genre)
            .Where(a =>
                a.Name.Contains(query) ||
                a.Biography.Contains(query))
            .ToListAsync();

        var musicItems = await _context.MusicItems
            .Include(m => m.Artist)
            .Include(m => m.Genre)
            .Where(m =>
                m.Title.Contains(query) ||
                m.Description.Contains(query) ||
                m.Type.Contains(query))
            .ToListAsync();

        var news = await _context.News
            .Where(n =>
                n.Title.Contains(query) ||
                n.Text.Contains(query))
            .ToListAsync();

        var mediaItems = await _context.MediaItems
            .Include(m => m.MusicItem)
            .ThenInclude(m => m!.Artist)
            .Where(m =>
                m.Title.Contains(query) ||
                m.Type.Contains(query) ||
                m.Description.Contains(query) ||
                m.UrlOrPath.Contains(query))
            .ToListAsync();

        ViewData["Query"] = query;

        var result = new SearchResultViewModel
        {
            Query = query,
            Genres = genres,
            Artists = artists,
            MusicItems = musicItems,
            News = news,
            MediaItems = mediaItems
        };

        return View(result);
    }
}