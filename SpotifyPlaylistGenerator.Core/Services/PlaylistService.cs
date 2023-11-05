using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IDbPlaylistService _dbPlaylistService;
    private readonly ISpotifyPlaylistService _spotifyPlaylistService;

    public PlaylistService(IDbPlaylistService dbPlaylistService, ISpotifyPlaylistService spotifyPlaylistService)
    {
        _dbPlaylistService = dbPlaylistService;
        _spotifyPlaylistService = spotifyPlaylistService;
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylists(string userId)
    {
        
        var spotifyPlaylistsTask = _spotifyPlaylistService.GetUserPlaylistCount(userId);
        var dbPlaylistsTask = _dbPlaylistService.GetUserPlaylistCount(userId);
        
        await Task.WhenAll(spotifyPlaylistsTask, dbPlaylistsTask);
        
        var spotifyPlaylistCount = await spotifyPlaylistsTask;
        var dbPlaylistCount = await dbPlaylistsTask;

        var fromDb = spotifyPlaylistCount == dbPlaylistCount;
        
        IBasePlaylistService fetchService = fromDb
            ? _dbPlaylistService
            : _spotifyPlaylistService;
        
        var playlists = await fetchService.GetUserPlaylists(userId);

        // var playlists = await _spotifyPlaylistService.GetUserPlaylists();
        
        // If not from DB, save Spotify results to DB.

        return playlists;
    }
}