using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbPlaylistTrackConverter
{
    public static (DbPlaylistTrack, DbTrack) ToDbPlaylistTrack(this PlaylistTrack playlistTrack)
    {
        return (new DbPlaylistTrack
        {
            PlaylistId = playlistTrack.PlaylistId, // Junction Reference
            PlaylistPosition = playlistTrack.PlaylistIndex,
            AddedAt = playlistTrack.AddedAt.ToUniversalTime(),
            TrackId = playlistTrack.Id // Junction Reference
        }, new DbTrack
        {
            Id = playlistTrack.Id,
            Name = playlistTrack.Name,
            AlbumId = playlistTrack.AlbumId,
            DiscNumber = playlistTrack.DiscNumber,
            TrackNumber = playlistTrack.TrackNumber,
            Explicit = playlistTrack.Explicit,
            DurationMs = playlistTrack.Duration,
            AssociatedArtists = playlistTrack.ArtistIds?.Select((artistId, index) => new DbArtistTrack { TrackId = playlistTrack.Id, ArtistId = artistId, ArtistIndex = index}).ToList()
        });
        
        // playlistTrack.Artists.Select(a => new DbArtistTrack { TrackId = a.Id})
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
            Duration = dbPlaylistTrack.Track.DurationMs,
            ArtistIds = dbPlaylistTrack.Track.AssociatedArtists.Select(a => a.ArtistId)
        };
    }
}