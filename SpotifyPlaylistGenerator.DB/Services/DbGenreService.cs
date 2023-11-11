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

    public Task AddGenre(DbGenre genre)
    {
        throw new NotImplementedException();
    }

    public Task AddGenres(IEnumerable<DbGenre> genres)
    {
        throw new NotImplementedException();
    }
}