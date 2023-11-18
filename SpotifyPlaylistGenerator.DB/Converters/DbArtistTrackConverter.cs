using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbArtistTrackConverter
{
    public static DbTrackArtist ToDbArtistTrack(this TrackArtist trackArtist)
    {
        return new DbTrackArtist
        {
            TrackId = trackArtist.TrackId,
            ArtistId = trackArtist.ArtistId,
            ArtistIndex = trackArtist.ArtistIndex
        };
    }

    public static TrackArtist ToTrack(this DbTrackArtist dbTrackArtist)
    {
        return new TrackArtist
        {
            TrackId = dbTrackArtist.TrackId,
            ArtistId = dbTrackArtist.ArtistId,
            ArtistIndex = dbTrackArtist.ArtistIndex
        };
    }
}