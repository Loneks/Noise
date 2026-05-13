namespace Noise.Models;

public class MediaItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Type { get; set; } = string.Empty;

    public string UrlOrPath { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int MusicItemId { get; set; }

    public MusicItem? MusicItem { get; set; }
}