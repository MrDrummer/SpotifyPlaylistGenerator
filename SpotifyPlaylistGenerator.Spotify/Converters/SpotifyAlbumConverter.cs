using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.DB;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Utilities;

namespace SpotifyPlaylistGenerator.Spotify.Converters;

public static class SpotifyAlbumConverter
{
    public static Album ToAlbum(this SimpleAlbum spotifyAlbum)
    {
        return new Album
        {
            Id = spotifyAlbum.Id,
            Name = spotifyAlbum.Name,
            Image = spotifyAlbum.Images[0].Url,
            AlbumType = AlbumType.Album, // spotifyAlbum.AlbumType
            ReleaseDate = DateHelper.ConvertToDateTime(spotifyAlbum.ReleaseDate),
            // GenreIds = spotifyAlbum.Genres,
            // TrackIds = spotifyAlbum.Tracks
        };
    }
}