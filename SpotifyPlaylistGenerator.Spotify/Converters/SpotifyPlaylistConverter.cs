using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Converters;

public static class SpotifyPlaylistConverter
{

    public static Playlist ToPlaylist(this FullPlaylist spotifyPlaylist)
    {
        return new Playlist
        {
            Id = spotifyPlaylist.Id,
            Name = spotifyPlaylist.Name,
            Image = spotifyPlaylist.Images[0].Url,
            SnapshotId = spotifyPlaylist.SnapshotId,
            TrackCount = spotifyPlaylist.Tracks.Total ?? 0,
            // OwnerId = spotifyPlaylist.Owner.Id,
            Public = spotifyPlaylist.Public ?? false
        };
    }
}