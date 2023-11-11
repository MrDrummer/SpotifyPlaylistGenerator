using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbArtistService : IDbArtistService
{
    private readonly SpotifyDbContext _context;

    public DbArtistService(SpotifyDbContext context)
    {
        _context = context;
    }

    public Task AddArtist(DbArtist artist)
    {
        throw new NotImplementedException();
    }

    public Task AddArtists(IEnumerable<DbArtist> artists)
    {
        throw new NotImplementedException();
    }
}