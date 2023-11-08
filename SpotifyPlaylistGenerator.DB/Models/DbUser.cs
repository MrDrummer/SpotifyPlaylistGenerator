using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

// Create an additional AppUser table to distinguish from generic Playlist creators. Can include settings and such there.

public class DbUser
{
    [Key]
    public string Id { get; set; } // mrdrummer25
    
    public string DisplayName { get; set; } // MrDrummer25
    public string Image { get; set; } // Profile pic
    // public int Followers { get; set; } // Gets updated anytime they log in, so it's fine.
    
    public ICollection<DbPlaylist> Playlists { get; set; }
}
