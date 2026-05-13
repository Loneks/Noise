namespace Noise.Models;

public class Genre
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int YearOfOrigin { get; set; }

    public int CountryId { get; set; }

    public Country? Country { get; set; }

    public ICollection<Artist> Artists { get; set; } = new List<Artist>();

    public ICollection<MusicItem> MusicItems { get; set; } = new List<MusicItem>();
}