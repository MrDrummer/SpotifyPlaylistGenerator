using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbAppUser
{
    [Key]
    public string AppUserId { get; set; }
    
    public ICollection<DbAppUserPlaylist> AssociatedPlaylists { get; set; }
    
    // Settings
    // LastLoggedIn
}