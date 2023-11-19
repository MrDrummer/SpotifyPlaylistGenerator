using SpotifyPlaylistGenerator.DB;
using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;
using SpotifyPlaylistGenerator.Utilities;

namespace SpotifyPlaylistGenerator.Core.Services;

public class PlaylistTrackService : IPlaylistTrackService
{
    private readonly ISpotifyTrackService _spotifyTrackService;
    private readonly ISpotifyAlbumService _spotifyAlbumService;
    private readonly ISpotifyArtistService _spotifyArtistService;
    
    private readonly IDbPlaylistTrackService _dbPlaylistTrackService;
    private readonly IDbPlaylistService _dbPlaylistService;
    private readonly IDbTrackService _dbTrackService;
    private readonly IDbAlbumService _dbAlbumService;
    private readonly IDbArtistService _dbArtistService;
    private readonly IDbTrackArtistService _dbTrackArtistService;
    private readonly IDbAlbumGenreService _dbAlbumGenreService;
    private readonly IDbArtistGenreService _dbArtistGenreService;
    private readonly IDbGenreService _dbGenreService;
    private readonly DbDebug _dbDebug;

    public PlaylistTrackService(
        ISpotifyTrackService spotifyTrackService,
        ISpotifyAlbumService spotifyAlbumService,
        ISpotifyArtistService spotifyArtistService,
        
        IDbPlaylistTrackService dbPlaylistTrackService,
        IDbPlaylistService dbPlaylistService,
        IDbTrackService dbTrackService,
        IDbAlbumService dbAlbumService,
        IDbArtistService dbArtistService,
        IDbTrackArtistService dbTrackArtistService,
        IDbAlbumGenreService dbAlbumGenreService,
        IDbArtistGenreService dbArtistGenreService,
        IDbGenreService dbGenreService,
        DbDebug dbDebug
    )
    {
        _spotifyTrackService = spotifyTrackService;
        _spotifyAlbumService = spotifyAlbumService;
        _spotifyArtistService = spotifyArtistService;
        
        _dbPlaylistTrackService = dbPlaylistTrackService;
        _dbPlaylistService = dbPlaylistService;
        _dbTrackService = dbTrackService;
        _dbAlbumService = dbAlbumService;
        _dbArtistService = dbArtistService;
        _dbGenreService = dbGenreService;
        _dbTrackArtistService = dbTrackArtistService;
        _dbAlbumGenreService = dbAlbumGenreService;
        _dbArtistGenreService = dbArtistGenreService;
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
        var playlistTracks = playlistTracksBasicMeta.PlaylistTracks.Select(pt => pt.ToDbPlaylistTrack());
        var trackArtists = playlistTracksBasicMeta.TrackArtists.Select(ta => ta.ToDbArtistTrack());
        // var playlists = playlistTracksBasicMeta.Playlists.Select(p => p.ToDbPlaylist());
        // var artistTracks = playlistTracksBasicMeta.TrackArtists.Select(at => at.ToDbArtistTrack());
        
        var artists = playlistTracksBasicMeta.Artists.Select(a => a.ToDbArtist());
        var artistIds = artists.Select(a => a.Id);
        var fullArtists = await artistIds.Distinct().ChunkAndProcessAsync(50, c => _spotifyArtistService.GetFullArtists(c));
        
        var allArtistGenres = fullArtists.SelectMany(fa => fa.Genres);

        var albums = playlistTracksBasicMeta.Albums.Select(a => a.ToDbAlbum());
        var albumIds = albums.Select(a => a.Id);
        var fullAlbums = await albumIds.Distinct().ChunkAndProcessAsync(20, c => _spotifyAlbumService.GetFullAlbums(c));
        
        var allAlbumGenres = fullAlbums.SelectMany(fa => fa.Genres);

        var artistGenres = new List<DbArtistGenre>();
        var albumGenres = new List<DbAlbumGenre>();
        
        foreach (var artist in fullArtists)
        {
            artistGenres.AddRange(artist.Genres.Select(ag => new DbArtistGenre
            {
                ArtistId = artist.Id,
                Name = ag
            }));
        }

        // TODO: Investigate if AlbumGenres are ever defined. From initial investigation, it appears as though this property is not populated!
        foreach (var album in fullAlbums)
        {
            albumGenres.AddRange(album.Genres.Select(ag => new DbAlbumGenre
            {
                AlbumId = album.Id,
                Name = ag
            }));
        }
        
        var genres = new List<string>();
        genres.AddRange(allArtistGenres);
        genres.AddRange(allAlbumGenres);

        Console.WriteLine("Artists Count: {0}", artists.Count());
        await _dbArtistService.AddArtists(artists);
        
        Console.WriteLine("Albums Count: {0}", albums.Count());
        await _dbAlbumService.AddAlbums(albums);
        
        Console.WriteLine("Track Count: {0}", tracks.Count());
        await _dbTrackService.AddTracks(tracks);
        
        Console.WriteLine("TrackArtists Count: {0}", trackArtists.Count());
        await _dbTrackArtistService.AddTrackArtists(trackArtists);
                
        Console.WriteLine("Genres Count: {0}", genres.Count());
        await _dbGenreService.AddGenres(genres.Select(g => new DbGenre
        {
            Name = g
        }));
        
        Console.WriteLine("AlbumGenres Count: {0}", albumGenres.Count);
        await _dbAlbumGenreService.AddAlbumGenres(albumGenres);
        
        Console.WriteLine("ArtistGenres Count: {0}", artistGenres.Count);
        await _dbArtistGenreService.AddArtistGenres(artistGenres);
        

        
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