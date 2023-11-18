using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbPlaylistTrackConverter
{
    public static DbPlaylistTrack ToDbPlaylistTrack(this PlaylistTrack playlistTrack)
    {
        return new DbPlaylistTrack
        {
            // Composite PK between PlaylistId, TrackId and PlaylistIndex
            PlaylistId = playlistTrack.PlaylistId, // Junction Reference
            TrackId = playlistTrack.TrackId, // Junction Reference
            PlaylistPosition = playlistTrack.PlaylistIndex,
            AddedAt = playlistTrack.AddedAt.ToUniversalTime()
        };
    }

    public static PlaylistTrack ToPlaylistTrack(this DbPlaylistTrack dbPlaylistTrack)
    {
        return new PlaylistTrack
        {
            
            PlaylistId = dbPlaylistTrack.PlaylistId,
            PlaylistIndex = dbPlaylistTrack.PlaylistPosition,
            AddedAt = dbPlaylistTrack.AddedAt,
            TrackId = dbPlaylistTrack.TrackId
        };
    }
}