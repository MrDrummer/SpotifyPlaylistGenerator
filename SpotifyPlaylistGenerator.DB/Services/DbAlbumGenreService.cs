using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbAlbumGenreService : IDbAlbumGenreService
{
    private readonly SpotifyDbContext _context;

    public DbAlbumGenreService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddAlbumGenre(DbAlbumGenre albumGenre)
    {
        await _context.AlbumGenres.AddIfNotExistsAsync(albumGenre, a =>
            a.AlbumId == albumGenre.AlbumId &&
            a.Genre == albumGenre.Genre
        );
        await _context.SaveChangesAsync();
    }

    public async Task AddAlbumGenres(IEnumerable<DbAlbumGenre> albumGenres)
    {
        await _context.AlbumGenres.AddIfNotExistsRangeAsync(albumGenres, entity =>
            e =>
                e.AlbumId == entity.AlbumId &&
                e.Genre == entity.Genre
        );
        await _context.SaveChangesAsync();
    }
}