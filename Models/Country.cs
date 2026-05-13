namespace Noise.Models;

public class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public ICollection<Artist> Artists { get; set; } = new List<Artist>();
}