namespace SpotifyPlaylistGenerator.Models.Models;

public class Genre
{
    public string Name { get; set; }
    
    public IEnumerable<Artist>? Artists { get; set; }
    public IEnumerable<Album>? Albums { get; set; }
}