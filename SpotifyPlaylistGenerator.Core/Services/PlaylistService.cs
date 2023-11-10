using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class PlaylistService : IPlaylistService
{
    private readonly IDbPlaylistService _dbPlaylistService;
    private readonly ISpotifyPlaylistService _spotifyPlaylistService;
    private readonly IDbAppUserPlaylistService _dbAppUserPlaylistService;

    public PlaylistService(IDbPlaylistService dbPlaylistService, ISpotifyPlaylistService spotifyPlaylistService, IDbAppUserPlaylistService appUserPlaylistService)
    {
        _dbPlaylistService = dbPlaylistService;
        _spotifyPlaylistService = spotifyPlaylistService;
        _dbAppUserPlaylistService = appUserPlaylistService;
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylists(string userId)
    {
        
        // var spotifyPlaylistsTask = _spotifyPlaylistService.GetUserPlaylistCount(userId);
        // var dbPlaylistsTask = _dbPlaylistService.GetUserPlaylistCount(userId);
        //
        // await Task.WhenAll(spotifyPlaylistsTask, dbPlaylistsTask);
        //
        // var spotifyPlaylistCount = await spotifyPlaylistsTask;
        // var dbPlaylistCount = await dbPlaylistsTask;

        // var fromDb = spotifyPlaylistCount == dbPlaylistCount;
        //
        // Console.WriteLine("Sourcing data from {0}.", fromDb ? "Database" : "Spotify");
        // if (fromDb)
        // {
        //     return await _dbPlaylistService.GetUserPlaylists(userId);
        // }
        
        var userPlaylists = await _spotifyPlaylistService.GetUserPlaylists(userId);

        var userPlaylistList = userPlaylists.ToList();
        
        // Will be inserted at auth time, so this should never create a new AppUser!
        await _dbAppUserPlaylistService.AddAppUserPlaylists(userPlaylistList.Select(p => p.ToDbPlaylist()), new DbAppUser
        {
            Id = userId
        });
        
        return userPlaylistList;

        // var playlists = await _spotifyPlaylistService.GetUserPlaylists();
    }

    public async Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistsChangeMeta(IEnumerable<Playlist> playlists)
    {
        return await _dbPlaylistService.GetPlaylistsChangeMeta(playlists);
    }

    public async Task<(int TrackCount, string SnapshotId)> GetPlaylistChangeMeta(Playlist playlists)
    {
        return await _dbPlaylistService.GetPlaylistChangeMeta(playlists);
    }
}