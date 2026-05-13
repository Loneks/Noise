using Microsoft.EntityFrameworkCore;
using Noise.Models;

namespace Noise.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Country> Countries { get; set; }

    public DbSet<Genre> Genres { get; set; }

    public DbSet<Artist> Artists { get; set; }

    public DbSet<MusicItem> MusicItems { get; set; }

    public DbSet<MediaItem> MediaItems { get; set; }

    public DbSet<News> News { get; set; }
}