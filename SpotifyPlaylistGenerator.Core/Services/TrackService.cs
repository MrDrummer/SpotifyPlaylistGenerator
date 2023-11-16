using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class TrackService : ITrackService
{
    private readonly ISpotifyTrackService _spotifyTrackService;
    private readonly IDbTrackService _dbTrackService;
    private readonly IDbAlbumService _dbAlbumService;
    private readonly IDbArtistService _dbArtistService;
    private readonly IDbGenreService _dbGenreService;
    private readonly IDbPlaylistService _dbPlaylistService;
    private readonly IPlaylistService _playlistService;

    public TrackService(IDbTrackService dbTrackService, ISpotifyTrackService spotifyTrackService, IDbAlbumService dbAlbumService, IDbArtistService dbArtistService, IDbGenreService dbGenreService, IDbPlaylistService dbPlaylistService, IPlaylistService playlistService)
    {
        _dbTrackService = dbTrackService;
        _spotifyTrackService = spotifyTrackService;
        _dbAlbumService = dbAlbumService;
        _dbArtistService = dbArtistService;
        _dbGenreService = dbGenreService;
        _dbPlaylistService = dbPlaylistService;
        _playlistService = playlistService;
    }
    
    public async Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        
        // TODO: Look into wrapping all of these statements below in Transactions so we can rollback in the event of any query erroring.
        // There is no destructive behaviour, so it isn't that big of a deal.

        var playlistTracksBasicMeta = await _spotifyTrackService.GetPlaylistTracksBasicMeta(playlistId);

        var artists = playlistTracksBasicMeta.UniqueArtists.Select(p => p.Value.ToDbArtist());
        var albums = playlistTracksBasicMeta.UniqueAlbums.Select(p => p.Value.ToDbAlbum());
        var playlistTracks = playlistTracksBasicMeta.PlaylistTracks.Select(pt => pt.ToDbPlaylistTrack());
        
        Console.WriteLine("Artists Count: {0}", artists.Count());
        await _dbArtistService.AddArtists(artists);
        Console.WriteLine("Albums Count: {0}", albums.Count());
        await _dbAlbumService.AddAlbums(albums);
        Console.WriteLine("PlaylistTrack Count: {0}", playlistTracks.Count());
        
        // Purge all PlaylistTrack entries so we don't end up with old Track positions lingering

        await _dbPlaylistService.RemovePlaylistTracks(playlistId);
        
        await _dbTrackService.AddPlaylistTracks(playlistTracks);

        var snapshotId = playlistTracksBasicMeta.SnapshotId;

        await _playlistService.UpdatePlaylistSnapshotId(playlistId, snapshotId);
        
        Console.WriteLine("GREAT SUCCESS!");
    }
}