using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbAlbumConverter
{
    public static DbAlbum ToDbAlbum(this Album artist)
    {
        return new DbAlbum
        {
            Id = artist.Id,
            Name = artist.Name,
            Image = artist.Image,
            ReleaseDate = artist.ReleaseDate?.ToUniversalTime()
        };
    }

    public static Album ToAlbum(this DbAlbum dbAlbum)
    {
        return new Album
        {
            Id = dbAlbum.Id,
            Name = dbAlbum.Name,
            Image = dbAlbum.Image,
            ReleaseDate = dbAlbum.ReleaseDate
        };
    }
}