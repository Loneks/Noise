namespace Noise.Models;

public class News
{
    public int Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Text { get; set; } = string.Empty;

    public DateTime PublishedAt { get; set; } = DateTime.Now;

    public string? ImageUrl { get; set; }
}