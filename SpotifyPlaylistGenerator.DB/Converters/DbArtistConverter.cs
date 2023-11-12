using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbArtistConverter
{
    public static DbArtist ToDbArtist(this Artist artist)
    {
        return new DbArtist
        {
            Id = artist.Id,
            Name = artist.Name,
            Image = artist.Image
        };
    }

    public static Artist ToArtist(this DbArtist dbArtist)
    {
        return new Artist
        {
            Id = dbArtist.Id,
            Name = dbArtist.Name,
            Image = dbArtist.Image
        };
    }
}