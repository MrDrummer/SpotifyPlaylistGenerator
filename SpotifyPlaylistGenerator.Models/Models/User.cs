namespace SpotifyPlaylistGenerator.Models.Models;

public class User
{
    public string Id { get; set; } // mrdrummer25
    
    public string DisplayName { get; set; } // MrDrummer25
    public string Image { get; set; } // Profile pic
    
    public AppUser? AppUser { get; set; }
    
    public IEnumerable<Playlist>? Playlists { get; set; }
    public IEnumerable<string>? PlaylistIds { get; set; }
}