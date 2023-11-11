using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class TrackService : ITrackService
{
    private readonly IDbTrackService _dbTrackService;
    private readonly ISpotifyTrackService _spotifyTrackService;

    public TrackService(IDbTrackService dbTrackService, ISpotifyTrackService spotifyTrackService)
    {
        _dbTrackService = dbTrackService;
        _spotifyTrackService = spotifyTrackService;
    }
    
    public async Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        // var spotifyTracksTask = _spotifyTrackService.GetPlaylistTrackCount(playlistId);
        // var dbTracksTask = _dbTrackService.GetPlaylistTrackCount(playlistId);
        //
        // await Task.WhenAll(spotifyTracksTask, dbTracksTask);
        //
        // var spotifyTrackCount = await spotifyTracksTask;
        // var dbTrackCount = await dbTracksTask;
        //
        // var fromDb = spotifyTrackCount == dbTrackCount;
        //
        // // if (fromDb)
        // {
        //     return await _dbTrackService.GetPlaylistTracksBasicMeta(playlistId);
        // }

        var playlistTracksBasicMeta = await _spotifyTrackService.GetPlaylistTracksBasicMeta(playlistId);

        // return tracks;
    }
}