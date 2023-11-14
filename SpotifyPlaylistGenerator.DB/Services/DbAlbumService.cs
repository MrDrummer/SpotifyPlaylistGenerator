using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbAlbumService : IDbAlbumService
{
    private readonly SpotifyDbContext _context;

    public DbAlbumService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddAlbum(DbAlbum album)
    {
        await _context.Albums.AddIfNotExistsAsync(album, a => a.Id == album.Id);
        await _context.SaveChangesAsync();
    }

    public async Task AddAlbums(IEnumerable<DbAlbum> albums)
    {
        await _context.Albums.AddIfNotExistsRangeAsync(albums.DistinctBy(a => a.Id), entity => e => e.Id == entity.Id);
        await _context.SaveChangesAsync();
    }
}