using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbTrackService : IDbTrackService
{
    public Task<int> GetTrackCount()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId)
    {
        throw new NotImplementedException();
    }
}