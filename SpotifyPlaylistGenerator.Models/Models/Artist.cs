namespace SpotifyPlaylistGenerator.Models.Models;

public class Artist // : User ?
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    
    // FullArtist
    public string Image { get; set; }
    
    public IEnumerable<Track>? Tracks { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
}