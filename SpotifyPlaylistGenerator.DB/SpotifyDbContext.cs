using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB;

public class SpotifyDbContext : DbContext
{
    public DbSet<DbPlaylist> Playlists { get; set; }
    public DbSet<DbTrack> Tracks { get; set; }
    
    public SpotifyDbContext(DbContextOptions<SpotifyDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
    }
}