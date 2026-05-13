namespace Noise.Models;

public class MusicItem
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int ReleaseYear { get; set; }

    public string Type { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int ArtistId { get; set; }

    public Artist? Artist { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; }

    public ICollection<MediaItem> MediaItems { get; set; } = new List<MediaItem>();
}