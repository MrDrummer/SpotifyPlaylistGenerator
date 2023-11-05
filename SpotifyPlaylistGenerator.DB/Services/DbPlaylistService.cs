using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbPlaylistService : IDbPlaylistService
{
    private readonly SpotifyDbContext _context;

    public DbPlaylistService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public Task<int> GetUserPlaylistCount()
    {
        throw new NotImplementedException();
    }
    
    public Task<IEnumerable<Playlist>> GetUserPlaylists()
    {
        throw new NotImplementedException();
    }
}