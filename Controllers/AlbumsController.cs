using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;

namespace Noise.Controllers;

public class AlbumsController : Controller
{
    private readonly ApplicationDbContext _context;

    public AlbumsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(int? genreId, int? artistId, int? year)
    {
        ViewData["Title"] = "Музичні альбоми";
        ViewData["SectionName"] = "Albums";

        ViewBag.Genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        ViewBag.Artists = await _context.Artists
            .OrderBy(a => a.Name)
            .ToListAsync();

        ViewBag.SelectedGenreId = genreId;
        ViewBag.SelectedArtistId = artistId;
        ViewBag.SelectedYear = year;

        var query = _context.MusicItems
            .Include(m => m.Artist)
            .Include(m => m.Genre)
            .Where(m => m.Type == "Альбом" || m.Type == "Album")
            .AsQueryable();

        if (genreId.HasValue)
        {
            query = query.Where(m => m.GenreId == genreId.Value);
        }

        if (artistId.HasValue)
        {
            query = query.Where(m => m.ArtistId == artistId.Value);
        }

        if (year.HasValue)
        {
            query = query.Where(m => m.ReleaseYear == year.Value);
        }

        var albums = await query
            .OrderByDescending(m => m.ReleaseYear)
            .ToListAsync();

        return View(albums);
    }

    public async Task<IActionResult> Details(int id)
    {
        var album = await _context.MusicItems
            .Include(m => m.Artist)
            .Include(m => m.Genre)
            .Include(m => m.MediaItems)
            .FirstOrDefaultAsync(m => m.Id == id);

        if (album == null)
        {
            return NotFound();
        }

        return View(album);
    }
}