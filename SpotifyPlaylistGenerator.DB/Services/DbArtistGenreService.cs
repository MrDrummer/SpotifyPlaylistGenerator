using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbArtistGenreService : IDbArtistGenreService
{
    private readonly SpotifyDbContext _context;

    public DbArtistGenreService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddArtistGenre(DbArtistGenre artistGenre)
    {
        await _context.ArtistGenres.AddIfNotExistsAsync(artistGenre, a =>
            a.ArtistId == artistGenre.ArtistId &&
            a.Genre == artistGenre.Genre
        );
        await _context.SaveChangesAsync();
    }

    public async Task AddArtistGenres(IEnumerable<DbArtistGenre> artistGenres)
    {
        await _context.ArtistGenres.AddIfNotExistsRangeAsync(artistGenres.DistinctBy(ag => new { ag.ArtistId, ag.Name }), entity =>
            e =>
                e.ArtistId == entity.ArtistId &&
                e.Name == entity.Name
            );
        await _context.SaveChangesAsync();
    }
}