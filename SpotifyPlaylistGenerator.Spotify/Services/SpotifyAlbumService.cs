using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Converters;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyAlbumService : ISpotifyAlbumService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public SpotifyAlbumService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }

    public async Task<Album> GetFullAlbum(string albumId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var album = await client.Albums.Get(albumId);

        return album.ToAlbum();
    }

    public async Task<IEnumerable<Album>> GetFullAlbums(IEnumerable<string> albumIds)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var albums = await client.Albums.GetSeveral(new AlbumsRequest(albumIds.ToList()));

        return albums.Albums.Select(a => a.ToAlbum());
    }
}