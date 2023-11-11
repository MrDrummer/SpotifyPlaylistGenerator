using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class TrackService : ITrackService
{
    private readonly IDbTrackService _dbTrackService;
    private readonly ISpotifyTrackService _spotifyTrackService;
    private readonly IDbAlbumService _dbAlbumService;
    private readonly IDbArtistService _dbArtistService;
    private readonly IDbGenreService _dbGenreService;

    public TrackService(IDbTrackService dbTrackService, ISpotifyTrackService spotifyTrackService, IDbAlbumService dbAlbumService, IDbArtistService dbArtistService, IDbGenreService dbGenreService)
    {
        _dbTrackService = dbTrackService;
        _spotifyTrackService = spotifyTrackService;
        _dbAlbumService = dbAlbumService;
        _dbArtistService = dbArtistService;
        _dbGenreService = dbGenreService;
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