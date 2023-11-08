using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbArtist
{
    [Key]
    public string Id { get; set; }
    
    public string Name { get; set; }
    public string Image { get; set; }
    // public int Followers { get; set; }
    // public int Popularity { get; set; }
    
    public ICollection<DbArtistGenre> AssociatedGenres { get; set; }
    public ICollection<DbArtistTrack> AssociatedTracks { get; set; }
}
