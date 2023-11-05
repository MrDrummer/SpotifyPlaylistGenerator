using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;
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
    
    public async Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId)
    {
        var spotifyTracksTask = _spotifyTrackService.GetTrackCount();
        var dbTracksTask = _dbTrackService.GetTrackCount();

        await Task.WhenAll(spotifyTracksTask, dbTracksTask);

        var spotifyTrackCount = await spotifyTracksTask;
        var dbTrackCount = await dbTracksTask;

        IBaseTrackService fetchService = spotifyTrackCount == dbTrackCount
            ? _spotifyTrackService
            : _dbTrackService;

        var tracks = await fetchService.GetPlaylistTracks(playlistId);

        return tracks;
    }
}