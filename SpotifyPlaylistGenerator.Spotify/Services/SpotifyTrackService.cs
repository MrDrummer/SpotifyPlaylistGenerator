using System.Data;
using System.Runtime.CompilerServices;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Spotify.Converters;
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
        var connector = await _spotifyServiceHolder.GetApiConnectorAsync();

        var firstQuery = await client.Playlists.Get(playlistId);

        var playlistTracks = new List<PlaylistTrack<IPlayableItem>>();
        // var playlistTracks = new Dictionary<string, PlaylistTrack<IPlayableItem>>();
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
            if (track.Track.Type != ItemType.Track) continue;
            var fullTrack = track.Track as FullTrack;

            if (fullTrack.Id == null) continue;
            
            playlistTracks.Add(track);
            // track.IsLocal
            var album = fullTrack.Album;

            if (album.Id == null)
            {
                throw new NoNullAllowedException($"Album ID is null?! Track ID: {fullTrack.Id}, Track Name: {fullTrack.Name}");
            }
            
            // playlistTracks.TryAdd(fullTrack.Id, track);
            uniqueAlbums.TryAdd(album.Id, album);
            
            fullTrack.Artists.ForEach(a => uniqueArtists.TryAdd(a.Id, a));
        }

        // TODO: For every 100 Tracks, fetch the Detailed Track info.
        // TODO: For every 100 Albums and Artists, fetch Full version
        
        // Genres are only available on the FULL model for Artist and Album.
        return new PlaylistTracksBasicMeta
        {
            SnapshotId = firstQuery.SnapshotId,
            // Need to have tracks and playlistTracks separately!
            
            PlaylistTracks = playlistTracks.Select((pt, index) => pt.ToPlaylist(playlistId, index)),
            // UniquePlaylistTracks = playlistTracks.ToDictionary(p => p.Key, p => p.Value.ToPlaylist(playlistId));
            UniqueAlbums = uniqueAlbums.ToDictionary(p => p.Key, p => p.Value.ToAlbum()),
            UniqueArtists = uniqueArtists.ToDictionary(p => p.Key, p => p.Value.ToArtist())
        };
        
    }

    private static async IAsyncEnumerable<PlaylistTrack<IPlayableItem>> Paginate(Paging<PlaylistTrack<IPlayableItem>>? paging,
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
            Console.WriteLine(firstPage);
            foreach (var track in firstPage.Items)
            {
                yield return track;
            }
        }
    }

    Task ITrackService.GetPlaylistTracksBasicMeta(string playlistId)
    {
        return GetPlaylistTracksBasicMeta(playlistId);
    }
}