using SpotifyPlaylistGenerator.DB;
using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Core.Services;

public class PlaylistTrackService : IPlaylistTrackService
{
    private readonly ISpotifyTrackService _spotifyTrackService;
    private readonly IDbTrackService _dbTrackService;
    private readonly IDbAlbumService _dbAlbumService;
    private readonly IDbArtistService _dbArtistService;
    private readonly IDbGenreService _dbGenreService;
    private readonly IDbPlaylistService _dbPlaylistService;
    private readonly IPlaylistService _playlistService;
    private readonly IDbPlaylistTrackService _dbPlaylistTrackService;
    private readonly IDbTrackArtistService _dbTrackArtistService;
    private readonly DbDebug _dbDebug;

    public PlaylistTrackService(
        IPlaylistService playlistService,
        ISpotifyTrackService spotifyTrackService,
        IDbPlaylistTrackService dbPlaylistTrackService,
        IDbPlaylistService dbPlaylistService,
        IDbTrackService dbTrackService,
        IDbAlbumService dbAlbumService,
        IDbArtistService dbArtistService,
        IDbGenreService dbGenreService,
        IDbTrackArtistService dbTrackArtistService,
        DbDebug dbDebug
    )
    {
        _playlistService = playlistService;
        _spotifyTrackService = spotifyTrackService;
        _dbPlaylistTrackService = dbPlaylistTrackService;
        _dbPlaylistService = dbPlaylistService;
        _dbTrackService = dbTrackService;
        _dbAlbumService = dbAlbumService;
        _dbArtistService = dbArtistService;
        _dbGenreService = dbGenreService;
        _dbTrackArtistService = dbTrackArtistService;
        _dbDebug = dbDebug;
    }
    
    public async Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        // TODO: Needs a refactor. I need to get all tracks in the playlist first and only get the metadata for tracks not already in the DB.
        // If I already have the track, I already have the Artist, Album and Genres.
        
        // TODO: Look into wrapping all of these statements below in Transactions so we can rollback in the event of any query erroring.
        // There is no destructive behaviour, so it isn't that big of a deal.

        var playlistTracksBasicMeta = await _spotifyTrackService.GetPlaylistTracksBasicMeta(playlistId);

        var tracks = playlistTracksBasicMeta.Tracks.Select(t => t.ToDbTrack());
        var albums = playlistTracksBasicMeta.Albums.Select(a => a.ToDbAlbum());
        var artists = playlistTracksBasicMeta.Artists.Select(a => a.ToDbArtist());
        var playlistTracks = playlistTracksBasicMeta.PlaylistTracks.Select(pt => pt.ToDbPlaylistTrack());
        var trackArtists = playlistTracksBasicMeta.TrackArtists.Select(ta => ta.ToDbArtistTrack());
        // var playlists = playlistTracksBasicMeta.Playlists.Select(p => p.ToDbPlaylist());
        // var artistTracks = playlistTracksBasicMeta.TrackArtists.Select(at => at.ToDbArtistTrack());

        Console.WriteLine("Artists Count: {0}", artists.Count());
        await _dbArtistService.AddArtists(artists);
        
        Console.WriteLine("Albums Count: {0}", albums.Count());
        await _dbAlbumService.AddAlbums(albums);
        
        Console.WriteLine("Track Count: {0}", tracks.Count());
        await _dbTrackService.AddTracks(tracks);
        
        
        Console.WriteLine("TrackArtists Count: {0}", trackArtists.Count());
        await _dbTrackArtistService.AddTrackArtists(trackArtists);
        
        // Purge all PlaylistTrack entries so we don't end up with old Track positions lingering

        
        await _dbPlaylistTrackService.RemovePlaylistTracks(playlistId);
        Console.WriteLine("PlaylistTrack Count: {0}", playlistTracks.Count());
        await _dbPlaylistTrackService.AddPlaylistTracks(playlistTracks);

        var snapshotId = playlistTracksBasicMeta.SnapshotId;
        
        // await _dbDebug.GetTrackedEntities();

        await _dbPlaylistService.UpdatePlaylistSnapshotId(playlistId, snapshotId);
        
        Console.WriteLine("GREAT SUCCESS!");
    }
}