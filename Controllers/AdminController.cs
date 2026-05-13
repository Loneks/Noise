using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Noise.Data;
using Noise.Models;

namespace Noise.Controllers;

public class AdminController : Controller
{
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Title"] = "Адмін-панель";
        ViewData["SectionName"] = "Admin";

        ViewBag.GenresCount = await _context.Genres.CountAsync();
        ViewBag.ArtistsCount = await _context.Artists.CountAsync();
        ViewBag.MusicItemsCount = await _context.MusicItems.CountAsync();
        ViewBag.NewsCount = await _context.News.CountAsync();
        ViewBag.MediaItemsCount = await _context.MediaItems.CountAsync();

        return View();
    }

    public async Task<IActionResult> News()
    {
        ViewData["Title"] = "Керування новинами";
        ViewData["SectionName"] = "Admin";

        var news = await _context.News
            .OrderByDescending(n => n.PublishedAt)
            .ToListAsync();

        return View(news);
    }

    [HttpGet]
    public IActionResult CreateNews()
    {
        ViewData["Title"] = "Додати новину";
        ViewData["SectionName"] = "Admin";

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNews(News news)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Додати новину";
            ViewData["SectionName"] = "Admin";

            return View(news);
        }

        news.PublishedAt = DateTime.Now;

        _context.News.Add(news);
        await _context.SaveChangesAsync();

        return RedirectToAction("News", "Admin");
    }

    [HttpGet]
    public async Task<IActionResult> EditNews(int id)
    {
        ViewData["Title"] = "Редагувати новину";
        ViewData["SectionName"] = "Admin";

        var newsItem = await _context.News.FindAsync(id);

        if (newsItem == null)
        {
            return NotFound();
        }

        return View(newsItem);
    }

    [HttpPost]
    public async Task<IActionResult> EditNews(News news)
    {
        if (!ModelState.IsValid)
        {
            ViewData["Title"] = "Редагувати новину";
            ViewData["SectionName"] = "Admin";

            return View(news);
        }

        var existingNews = await _context.News.FindAsync(news.Id);

        if (existingNews == null)
        {
            return NotFound();
        }

        existingNews.Title = news.Title;
        existingNews.Text = news.Text;
        existingNews.ImageUrl = news.ImageUrl;

        await _context.SaveChangesAsync();

        return RedirectToAction("News", "Admin");
    }

    [HttpPost]
    public async Task<IActionResult> DeleteNews(int id)
    {
        var newsItem = await _context.News.FindAsync(id);

        if (newsItem == null)
        {
            return NotFound();
        }

        _context.News.Remove(newsItem);
        await _context.SaveChangesAsync();

        return RedirectToAction("News", "Admin");
    }

    public async Task<IActionResult> Genres()
{
    ViewData["Title"] = "Керування жанрами";
    ViewData["SectionName"] = "Admin";

    var genres = await _context.Genres
        .Include(g => g.Country)
        .OrderBy(g => g.Name)
        .ToListAsync();

    return View(genres);
}

[HttpGet]
public async Task<IActionResult> CreateGenre()
{
    ViewData["Title"] = "Додати жанр";
    ViewData["SectionName"] = "Admin";

    ViewBag.Countries = await _context.Countries
        .OrderBy(c => c.Name)
        .ToListAsync();

    return View();
}

[HttpPost]
public async Task<IActionResult> CreateGenre(Genre genre)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Додати жанр";
        ViewData["SectionName"] = "Admin";

        ViewBag.Countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(genre);
    }

    _context.Genres.Add(genre);
    await _context.SaveChangesAsync();

    return RedirectToAction("Genres", "Admin");
}

[HttpGet]
public async Task<IActionResult> EditGenre(int id)
{
    ViewData["Title"] = "Редагувати жанр";
    ViewData["SectionName"] = "Admin";

    var genre = await _context.Genres.FindAsync(id);

    if (genre == null)
    {
        return NotFound();
    }

    ViewBag.Countries = await _context.Countries
        .OrderBy(c => c.Name)
        .ToListAsync();

    return View(genre);
}

[HttpPost]
public async Task<IActionResult> EditGenre(Genre genre)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Редагувати жанр";
        ViewData["SectionName"] = "Admin";

        ViewBag.Countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();

        return View(genre);
    }

    var existingGenre = await _context.Genres.FindAsync(genre.Id);

    if (existingGenre == null)
    {
        return NotFound();
    }

    existingGenre.Name = genre.Name;
    existingGenre.Description = genre.Description;
    existingGenre.YearOfOrigin = genre.YearOfOrigin;
    existingGenre.CountryId = genre.CountryId;

    await _context.SaveChangesAsync();

    return RedirectToAction("Genres", "Admin");
}

[HttpPost]
public async Task<IActionResult> DeleteGenre(int id)
{
    var genre = await _context.Genres.FindAsync(id);

    if (genre == null)
    {
        return NotFound();
    }

    _context.Genres.Remove(genre);
    await _context.SaveChangesAsync();

    return RedirectToAction("Genres", "Admin");
}

public async Task<IActionResult> Artists()
{
    ViewData["Title"] = "Керування виконавцями";
    ViewData["SectionName"] = "Admin";

    var artists = await _context.Artists
        .Include(a => a.Country)
        .Include(a => a.Genre)
        .OrderBy(a => a.Name)
        .ToListAsync();

    return View(artists);
}

[HttpGet]
public async Task<IActionResult> CreateArtist()
{
    ViewData["Title"] = "Додати виконавця";
    ViewData["SectionName"] = "Admin";

    ViewBag.Countries = await _context.Countries
        .OrderBy(c => c.Name)
        .ToListAsync();

    ViewBag.Genres = await _context.Genres
        .OrderBy(g => g.Name)
        .ToListAsync();

    return View();
}

[HttpPost]
public async Task<IActionResult> CreateArtist(Artist artist)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Додати виконавця";
        ViewData["SectionName"] = "Admin";

        ViewBag.Countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();

        ViewBag.Genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(artist);
    }

    _context.Artists.Add(artist);
    await _context.SaveChangesAsync();

    return RedirectToAction("Artists", "Admin");
}

[HttpGet]
public async Task<IActionResult> EditArtist(int id)
{
    ViewData["Title"] = "Редагувати виконавця";
    ViewData["SectionName"] = "Admin";

    var artist = await _context.Artists.FindAsync(id);

    if (artist == null)
    {
        return NotFound();
    }

    ViewBag.Countries = await _context.Countries
        .OrderBy(c => c.Name)
        .ToListAsync();

    ViewBag.Genres = await _context.Genres
        .OrderBy(g => g.Name)
        .ToListAsync();

    return View(artist);
}

[HttpPost]
public async Task<IActionResult> EditArtist(Artist artist)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Редагувати виконавця";
        ViewData["SectionName"] = "Admin";

        ViewBag.Countries = await _context.Countries
            .OrderBy(c => c.Name)
            .ToListAsync();

        ViewBag.Genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(artist);
    }

    var existingArtist = await _context.Artists.FindAsync(artist.Id);

    if (existingArtist == null)
    {
        return NotFound();
    }

    existingArtist.Name = artist.Name;
    existingArtist.StartYear = artist.StartYear;
    existingArtist.EndYear = artist.EndYear;
    existingArtist.Biography = artist.Biography;
    existingArtist.CountryId = artist.CountryId;
    existingArtist.GenreId = artist.GenreId;

    await _context.SaveChangesAsync();

    return RedirectToAction("Artists", "Admin");
}

[HttpPost]
public async Task<IActionResult> DeleteArtist(int id)
{
    var artist = await _context.Artists.FindAsync(id);

    if (artist == null)
    {
        return NotFound();
    }

    _context.Artists.Remove(artist);
    await _context.SaveChangesAsync();

    return RedirectToAction("Artists", "Admin");
}

public async Task<IActionResult> MusicItems()
{
    ViewData["Title"] = "Керування альбомами";
    ViewData["SectionName"] = "Admin";

    var musicItems = await _context.MusicItems
        .Include(m => m.Artist)
        .Include(m => m.Genre)
        .OrderByDescending(m => m.ReleaseYear)
        .ToListAsync();

    return View(musicItems);
}

[HttpGet]
public async Task<IActionResult> CreateMusicItem()
{
    ViewData["Title"] = "Додати альбом";
    ViewData["SectionName"] = "Admin";

    ViewBag.Artists = await _context.Artists
        .OrderBy(a => a.Name)
        .ToListAsync();

    ViewBag.Genres = await _context.Genres
        .OrderBy(g => g.Name)
        .ToListAsync();

    return View();
}

[HttpPost]
public async Task<IActionResult> CreateMusicItem(MusicItem musicItem)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Додати альбом";
        ViewData["SectionName"] = "Admin";

        ViewBag.Artists = await _context.Artists
            .OrderBy(a => a.Name)
            .ToListAsync();

        ViewBag.Genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(musicItem);
    }

    _context.MusicItems.Add(musicItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("MusicItems", "Admin");
}

[HttpGet]
public async Task<IActionResult> EditMusicItem(int id)
{
    ViewData["Title"] = "Редагувати альбом";
    ViewData["SectionName"] = "Admin";

    var musicItem = await _context.MusicItems.FindAsync(id);

    if (musicItem == null)
    {
        return NotFound();
    }

    ViewBag.Artists = await _context.Artists
        .OrderBy(a => a.Name)
        .ToListAsync();

    ViewBag.Genres = await _context.Genres
        .OrderBy(g => g.Name)
        .ToListAsync();

    return View(musicItem);
}

[HttpPost]
public async Task<IActionResult> EditMusicItem(MusicItem musicItem)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Редагувати альбом";
        ViewData["SectionName"] = "Admin";

        ViewBag.Artists = await _context.Artists
            .OrderBy(a => a.Name)
            .ToListAsync();

        ViewBag.Genres = await _context.Genres
            .OrderBy(g => g.Name)
            .ToListAsync();

        return View(musicItem);
    }

    var existingMusicItem = await _context.MusicItems.FindAsync(musicItem.Id);

    if (existingMusicItem == null)
    {
        return NotFound();
    }

    existingMusicItem.Title = musicItem.Title;
    existingMusicItem.ReleaseYear = musicItem.ReleaseYear;
    existingMusicItem.Type = musicItem.Type;
    existingMusicItem.Description = musicItem.Description;
    existingMusicItem.ArtistId = musicItem.ArtistId;
    existingMusicItem.GenreId = musicItem.GenreId;

    await _context.SaveChangesAsync();

    return RedirectToAction("MusicItems", "Admin");
}

[HttpPost]
public async Task<IActionResult> DeleteMusicItem(int id)
{
    var musicItem = await _context.MusicItems.FindAsync(id);

    if (musicItem == null)
    {
        return NotFound();
    }

    _context.MusicItems.Remove(musicItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("MusicItems", "Admin");
}

public async Task<IActionResult> MediaItems()
{
    ViewData["Title"] = "Керування інформаційними матеріалами";
    ViewData["SectionName"] = "Admin";

    var mediaItems = await _context.MediaItems
        .Include(m => m.MusicItem)
        .ThenInclude(m => m!.Artist)
        .OrderBy(m => m.Type)
        .ThenBy(m => m.Title)
        .ToListAsync();

    return View(mediaItems);
}

[HttpGet]
public async Task<IActionResult> CreateMediaItem()
{
    ViewData["Title"] = "Додати інформаційний матеріал";
    ViewData["SectionName"] = "Admin";

    ViewBag.MusicItems = await _context.MusicItems
        .Include(m => m.Artist)
        .OrderBy(m => m.Title)
        .ToListAsync();

    return View();
}

[HttpPost]
public async Task<IActionResult> CreateMediaItem(MediaItem mediaItem)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Додати інформаційний матеріал";
        ViewData["SectionName"] = "Admin";

        ViewBag.MusicItems = await _context.MusicItems
            .Include(m => m.Artist)
            .OrderBy(m => m.Title)
            .ToListAsync();

        return View(mediaItem);
    }

    _context.MediaItems.Add(mediaItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("MediaItems", "Admin");
}

[HttpGet]
public async Task<IActionResult> EditMediaItem(int id)
{
    ViewData["Title"] = "Редагувати інформаційний матеріал";
    ViewData["SectionName"] = "Admin";

    var mediaItem = await _context.MediaItems.FindAsync(id);

    if (mediaItem == null)
    {
        return NotFound();
    }

    ViewBag.MusicItems = await _context.MusicItems
        .Include(m => m.Artist)
        .OrderBy(m => m.Title)
        .ToListAsync();

    return View(mediaItem);
}

[HttpPost]
public async Task<IActionResult> EditMediaItem(MediaItem mediaItem)
{
    if (!ModelState.IsValid)
    {
        ViewData["Title"] = "Редагувати інформаційний матеріал";
        ViewData["SectionName"] = "Admin";

        ViewBag.MusicItems = await _context.MusicItems
            .Include(m => m.Artist)
            .OrderBy(m => m.Title)
            .ToListAsync();

        return View(mediaItem);
    }

    var existingMediaItem = await _context.MediaItems.FindAsync(mediaItem.Id);

    if (existingMediaItem == null)
    {
        return NotFound();
    }

    existingMediaItem.Title = mediaItem.Title;
    existingMediaItem.Type = mediaItem.Type;
    existingMediaItem.UrlOrPath = mediaItem.UrlOrPath;
    existingMediaItem.Description = mediaItem.Description;
    existingMediaItem.MusicItemId = mediaItem.MusicItemId;

    await _context.SaveChangesAsync();

    return RedirectToAction("MediaItems", "Admin");
}

[HttpPost]
public async Task<IActionResult> DeleteMediaItem(int id)
{
    var mediaItem = await _context.MediaItems.FindAsync(id);

    if (mediaItem == null)
    {
        return NotFound();
    }

    _context.MediaItems.Remove(mediaItem);
    await _context.SaveChangesAsync();

    return RedirectToAction("MediaItems", "Admin");
}
}