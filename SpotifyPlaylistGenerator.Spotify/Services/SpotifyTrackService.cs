using System.Runtime.CompilerServices;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyTrackService : ISpotifyTrackService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public SpotifyTrackService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }
    
    public async Task<int> GetPlaylistTrackCount(string playlistId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        
        var playlist = await client.Playlists.Get(playlistId);

        return playlist.Tracks?.Total ?? 0;
    }
    
    public async Task<PlaylistTracksBasicMeta> GetPlaylistTracksBasicMeta(string playlistId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        var connector = _spotifyServiceHolder.GetApiConnector();

        var firstQuery = await client.Playlists.Get(playlistId);

        var playlistTracks = new List<PlaylistTrack<IPlayableItem>>();
        var uniqueAlbums = new Dictionary<string, SimpleAlbum>();
        var uniqueArtists = new Dictionary<string, SimpleArtist>();
        /*
         * 1. Recursive fetch Playlist Tracks
         * 2. Figure out where Track Detail comes in
         * 3. Recursive fetch Track Album
         * 4. Recursive fetch Track Artist
         * 5. Handle Album Genres
         * 6. Handle Artist Genres
         */
        await foreach (var track in Paginate(firstQuery.Tracks, connector))
        {
            playlistTracks.Add(track);
            if (track.Track.Type != ItemType.Track) continue;
            var fullTrack = track.Track as FullTrack;
            // track.IsLocal
            var album = fullTrack.Album;
            
            uniqueAlbums.TryAdd(album.Id, album);
            
            album.Artists.ForEach(a => uniqueArtists.TryAdd(a.Id, a));
        }

        // Genres are only available on the FULL model for Artist and Album.
        return new PlaylistTracksBasicMeta
        {
            // PlaylistTracks = playlistTracks
            // UniqueAlbums = uniqueAlbums
            // UniqueArtists = uniqueArtists
        };
        
    }

    private async IAsyncEnumerable<PlaylistTrack<IPlayableItem>> Paginate(Paging<PlaylistTrack<IPlayableItem>>? paging,
        IAPIConnector connector, [EnumeratorCancellation] CancellationToken cancel = default)
    {
        var firstPage = paging;
        foreach (var track in firstPage.Items)
        {
            yield return track;
        }

        while (firstPage.Next != null)
        {
            firstPage = await connector
                .Get<Paging<PlaylistTrack<IPlayableItem>>>(new Uri(firstPage.Next, UriKind.Absolute), cancel)
                .ConfigureAwait(false);
            foreach (var track in firstPage.Items)
            {
                yield return track;
            }
        }
    }
}