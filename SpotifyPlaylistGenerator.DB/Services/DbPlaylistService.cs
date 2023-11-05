using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbPlaylistService : IDbPlaylistService
{
    public Task<int> GetUserPlaylistCount()
    {
        throw new NotImplementedException();
    }
    
    public Task<IEnumerable<Playlist>> GetUserPlaylists()
    {
        throw new NotImplementedException();
    }
}