using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbArtist
{
    [Key]
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    // FullArtist
    public string Image { get; set; }
    // public int Followers { get; set; }
    // public int Popularity { get; set; }
    
    // FullArtist
    public ICollection<DbArtistGenre> AssociatedGenres { get; set; }
    
    // Only available from Spotify when looking up Track
    public ICollection<DbArtistTrack> AssociatedTracks { get; set; }
}
