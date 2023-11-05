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

    public async Task<IEnumerable<Playlist>> GetUserPlaylists()
    {
        // var spotifyPlaylistsTask = _spotifyPlaylistService.GetUserPlaylistCount();
        // var dbPlaylistsTask = _dbPlaylistService.GetUserPlaylistCount();
        //
        // await Task.WhenAll(spotifyPlaylistsTask, dbPlaylistsTask);
        //
        // var spotifyPlaylistCount = await spotifyPlaylistsTask;
        // var dbPlaylistCount = await dbPlaylistsTask;
        //
        // IBasePlaylistService fetchService = spotifyPlaylistCount == dbPlaylistCount
        //     ? _spotifyPlaylistService
        //     : _dbPlaylistService;
        //
        // var playlists = await fetchService.GetUserPlaylists();

        var playlists = await _spotifyPlaylistService.GetUserPlaylists();

        return playlists;
    }
}