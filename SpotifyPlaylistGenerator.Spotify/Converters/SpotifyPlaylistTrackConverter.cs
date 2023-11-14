using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Converters;

public static class SpotifyPlaylistTrackConverter
{
    public static PlaylistTrack ToPlaylist(this PlaylistTrack<IPlayableItem> spotifyPlaylistTrack, string playlistId, int playlistIndex)
    {
        
        if (spotifyPlaylistTrack.Track.Type != ItemType.Track) throw new Exception("Playlist Item is not of type Track");

        if (spotifyPlaylistTrack.AddedAt == null) throw new Exception("Date is not valid");
        
        var fullTrack = spotifyPlaylistTrack.Track as FullTrack;
        return new PlaylistTrack
        {
            // TrackId
            Id = fullTrack.Id,
            Name = fullTrack.Name,
            Explicit = fullTrack.Explicit,
            Duration = fullTrack.DurationMs,
            DiscNumber = fullTrack.DiscNumber,
            TrackNumber = fullTrack.TrackNumber,
            AlbumId = fullTrack.Album.Id,
            ArtistIds = fullTrack.Artists.Select(a => a.Id),
            AddedAt = spotifyPlaylistTrack.AddedAt.Value,
            PlaylistIndex = playlistIndex,
            PlaylistId = playlistId
        };
    }
}