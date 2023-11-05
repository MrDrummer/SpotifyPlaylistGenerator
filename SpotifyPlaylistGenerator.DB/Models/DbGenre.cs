using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbGenre
{
    [Key]
    public int GenreId { get; set; }
    
    public string Name { get; set; }
    
    public ICollection<DbArtistGenre> AssociatedArtists { get; set; }
    public ICollection<DbAlbumGenre> AssociatedAlbums { get; set; }
}