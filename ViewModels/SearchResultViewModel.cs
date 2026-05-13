using Noise.Models;

namespace Noise.ViewModels;

public class SearchResultViewModel
{
    public string? Query { get; set; }

    public List<Genre> Genres { get; set; } = new();
    public List<Artist> Artists { get; set; } = new();
    public List<MusicItem> MusicItems { get; set; } = new();
    public List<News> News { get; set; } = new();
    public List<MediaItem> MediaItems { get; set; } = new();
}