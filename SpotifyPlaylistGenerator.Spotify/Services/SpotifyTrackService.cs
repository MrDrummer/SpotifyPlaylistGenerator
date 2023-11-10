using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyTrackService : ISpotifyTrackService
{
    public Task<int> GetTrackCount()
    {
        throw new NotImplementedException();
    }
    
    public Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId)
    {
        throw new NotImplementedException();
    }

    public Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistChangeMeta(IEnumerable<Playlist> playlists)
    {
        throw new NotImplementedException();
    }
}