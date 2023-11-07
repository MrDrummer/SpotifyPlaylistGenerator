using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbAlbumGenre
{
    // [Key]
    // public int AlbumGenreId { get; set; }
    
    public int GenreId { get; set; }
    public DbGenre Genre { get; set; }
    
    public string AlbumId { get; set; }
    public DbAlbum Album { get; set; }
}