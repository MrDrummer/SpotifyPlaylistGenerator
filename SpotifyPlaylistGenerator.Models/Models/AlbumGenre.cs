namespace SpotifyPlaylistGenerator.Models.Models;

public class AlbumGenre
{
    
    public string AlbumId { get; set; }
    public Album? Album { get; set; }
    
    public string Genre { get; set; }
}