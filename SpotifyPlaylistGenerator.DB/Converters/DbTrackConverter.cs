using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbTrackConverter
{
    public static DbTrack ToDbTrack(this Track track)
    {
        return new DbTrack
        {
            Id = track.Id,
            Name = track.Name,
            AlbumId = track.AlbumId,
            DiscNumber = track.DiscNumber,
            TrackNumber = track.TrackNumber,
            Explicit = track.Explicit,
            DurationMs = track.Duration,
            AssociatedArtists = track.ArtistIds?.Select(artistId => new DbArtistTrack { TrackId = track.Id, ArtistId = artistId}).ToList()
        };
    }

    public static Track ToTrack(this DbTrack dbTrack)
    {
        return new Track
        {
            Id = dbTrack.Id,
            Name = dbTrack.Name,
            DiscNumber = dbTrack.DiscNumber,
            TrackNumber = dbTrack.TrackNumber,
            Explicit = dbTrack.Explicit,
            Duration = dbTrack.DurationMs,
            AlbumId = dbTrack.AlbumId,
            ArtistIds = dbTrack.AssociatedArtists.Select(a => a.ArtistId)
        };
    }
}