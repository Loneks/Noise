namespace Noise.Models;

public class Artist
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int StartYear { get; set; }

    public int? EndYear { get; set; }

    public string Biography { get; set; } = string.Empty;

    public int CountryId { get; set; }

    public Country? Country { get; set; }

    public int GenreId { get; set; }

    public Genre? Genre { get; set; }

    public ICollection<MusicItem> MusicItems { get; set; } = new List<MusicItem>();
}