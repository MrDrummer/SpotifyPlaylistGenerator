﻿using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Converters;

public static class SpotifyArtistConverter
{
    public static Artist ToArtist(this SimpleArtist spotifyArtist)
    {
        return new Artist
        {
            Id = spotifyArtist.Id,
            Name = spotifyArtist.Name,
            // Image = spotifyArtist.Images[0].Url
        };
    }
}