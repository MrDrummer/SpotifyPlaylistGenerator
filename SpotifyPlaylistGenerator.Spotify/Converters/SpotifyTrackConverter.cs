using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Converters;

public static class SpotifyTrackConverter
{
    
    public static Track ToTrack(this FullTrack spotifyTrack)
    {
        return new Track
        {
            // TrackId
            Id = spotifyTrack.Id,
            Name = spotifyTrack.Name,
            Explicit = spotifyTrack.Explicit,
            Duration = spotifyTrack.DurationMs,
            DiscNumber = spotifyTrack.DiscNumber,
            TrackNumber = spotifyTrack.TrackNumber,
            AlbumId = spotifyTrack.Album.Id,
            ArtistIds = spotifyTrack.Artists.Select(a => a.Id),
        };
    }
}