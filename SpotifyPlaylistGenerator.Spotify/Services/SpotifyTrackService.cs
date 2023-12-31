﻿using System.Data;
using System.Runtime.CompilerServices;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;
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

        var tracksDict = new Dictionary<string, FullTrack>();
        var albumsDict = new Dictionary<string, SimpleAlbum>();
        var artistsDict = new Dictionary<string, SimpleArtist>();
        var playlistTracksList = new List<PlaylistTrack<IPlayableItem>>();
        var trackArtistsDict = new Dictionary<string, IEnumerable<string>>();
        
        await foreach (var track in Paginate(firstQuery.Tracks, connector))
        {
            if (track.Track.Type != ItemType.Track) continue;
            var fullTrack = track.Track as FullTrack;

            if (fullTrack?.Id == null) continue;
            // track.IsLocal
            
            playlistTracksList.Add(track);
            tracksDict.TryAdd(fullTrack.Id, fullTrack);
            var album = fullTrack.Album;

            if (album.Id == null)
            {
                throw new NoNullAllowedException($"Album ID is null?! Track ID: {fullTrack.Id}, Track Name: {fullTrack.Name}");
            }
            
            albumsDict.TryAdd(album.Id, album);
            
            fullTrack.Artists.ForEach(a => artistsDict.TryAdd(a.Id, a));
            trackArtistsDict.TryAdd(fullTrack.Id, fullTrack.Artists.Select(a => a.Id));
        }





        var trackArtists = trackArtistsDict.SelectMany(kvp =>
            kvp.Value.Select((a, index) => new TrackArtist { TrackId = kvp.Key, ArtistId = a, ArtistIndex = index }));
        
        // Genres are only available on the FULL model for Artist and Album.
        return new PlaylistTracksBasicMeta
        {
            SnapshotId = firstQuery.SnapshotId,
            // Need to have tracks and playlistTracks separately!
            Tracks = tracksDict.Select(t => t.Value.ToTrack()),
            // UniquePlaylistTracks = playlistTracks.ToDictionary(p => p.Key, p => p.Value.ToPlaylist(playlistId));
            Albums = albumsDict.Select(p => p.Value.ToAlbum()),
            Artists = artistsDict.Select(p => p.Value.ToArtist()),
            PlaylistTracks = playlistTracksList.Select((pt, index) => pt.ToPlaylist(playlistId, index)),
            TrackArtists = trackArtists.DistinctBy(ta => new { ta.ArtistId, ta.TrackId })
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

    public async Task<Track> GetFullTrack(string trackId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var track = await client.Tracks.Get(trackId);

        return track.ToTrack();
    }

    public async Task<IEnumerable<Track>> GetFullTracks(IEnumerable<string> trackIds)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var tracks = await client.Tracks.GetSeveral(new TracksRequest(trackIds.ToList()));

        return tracks.Tracks.Select(a => a.ToTrack());
    }

    // TODO: How to structure TrackAudioFeatures within the database?
    public async Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<TrackAudioFeatures>> GetTracksAudioFeatures(IEnumerable<string> trackIds)
    {
        throw new NotImplementedException();
    }
}