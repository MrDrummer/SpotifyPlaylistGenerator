using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Converters;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyPlaylistService : ISpotifyPlaylistService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public SpotifyPlaylistService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }

    public async Task<int> GetUserPlaylistCount(string userId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        
        var firstPlaylistPage = await client.Playlists.CurrentUsers();

        return firstPlaylistPage.Total ?? 0;
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylists(string userId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var firstPlaylistPage = await client.Playlists.CurrentUsers();
        
        var playlists = (await client.PaginateAll(firstPlaylistPage)).ToList();
        
        return playlists.Select(p => p.ToPlaylist());
    }

    public async Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistsChangeMeta(IEnumerable<Playlist> playlists)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        throw new NotImplementedException();
    }

    public async Task<(int TrackCount, string SnapshotId)> GetPlaylistChangeMeta(Playlist playlist)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        throw new NotImplementedException();
    }

    public Task UpdatePlaylistSnapshotId(string playlistId, string snapshotId)
    {
        throw new NotImplementedException();
    }
}