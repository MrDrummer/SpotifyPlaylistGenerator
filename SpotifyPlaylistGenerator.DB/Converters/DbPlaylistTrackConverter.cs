using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbPlaylistTrackConverter
{
    public static DbPlaylistTrack ToDbPlaylistTrack(this PlaylistTrack playlistTrack)
    {
        return new DbPlaylistTrack
        {
            PlaylistId = playlistTrack.PlaylistId, // Junction Reference
            PlaylistPosition = playlistTrack.PlaylistIndex,
            AddedAt = playlistTrack.AddedAt,
            TrackId = playlistTrack.Id // Junction Reference
        };
    }

    public static PlaylistTrack ToPlaylistTrack(this DbPlaylistTrack dbPlaylistTrack)
    {
        return new PlaylistTrack
        {
            PlaylistId = dbPlaylistTrack.PlaylistId,
            PlaylistIndex = dbPlaylistTrack.PlaylistPosition,
            AddedAt = dbPlaylistTrack.AddedAt,
            Id = dbPlaylistTrack.TrackId,
            Name = dbPlaylistTrack.Track.Name,
            AlbumId = dbPlaylistTrack.Track.AlbumId,
            DiscNumber = dbPlaylistTrack.Track.DiscNumber,
            TrackNumber = dbPlaylistTrack.Track.TrackNumber,
            Explicit = dbPlaylistTrack.Track.Explicit,
            Duration = dbPlaylistTrack.Track.DurationMs
        };
    }
}