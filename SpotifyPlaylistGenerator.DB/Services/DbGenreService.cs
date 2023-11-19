using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbGenreService : IDbGenreService
{
    private readonly SpotifyDbContext _context;

    public DbGenreService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddGenre(DbGenre genre)
    {
        await _context.Genres.AddIfNotExistsAsync(genre, g => g.Name == genre.Name);
        await _context.SaveChangesAsync();
    }

    public async Task AddGenres(IEnumerable<DbGenre> genres)
    {
        await _context.Genres.AddIfNotExistsRangeAsync(genres.DistinctBy(g => g.Name), entity => e => e.Name == entity.Name);
        await _context.SaveChangesAsync();
    }
}