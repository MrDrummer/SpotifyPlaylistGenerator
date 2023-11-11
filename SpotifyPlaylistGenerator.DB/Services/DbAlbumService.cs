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

    public Task AddAlbum(DbAlbum album)
    {
        throw new NotImplementedException();
    }

    public Task AddAlbums(IEnumerable<DbAlbum> albums)
    {
        throw new NotImplementedException();
    }
}