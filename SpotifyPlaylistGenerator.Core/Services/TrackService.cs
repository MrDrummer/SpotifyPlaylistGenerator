using SpotifyPlaylistGenerator.DB.Converters;
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

        var playlists = playlistTracksBasicMeta.PlaylistTracks.Select(pt => pt.ToDbPlaylistTrack());
        var artists = playlistTracksBasicMeta.UniqueArtists.ToDictionary(p => p.Key, p => p.Value.ToDbArtist());
        var albums = playlistTracksBasicMeta.UniqueAlbums.ToDictionary(p => p.Key, p => p.Value.ToDbAlbum());
        
        Console.WriteLine("Playlist Count: {0}", playlists.Count());
        Console.WriteLine("Artists Count: {0}", artists.Count);
        Console.WriteLine("Albums Count: {0}", albums.Count);
        

        // return tracks;
    }
}