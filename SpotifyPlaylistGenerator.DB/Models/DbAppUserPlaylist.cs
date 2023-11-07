using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbAppUserPlaylist
{
    // [Key]
    // public int AppUserPlaylistId { get; set; }
    
    public string AppUserId { get; set; }
    public DbAppUser AppUser { get; set; }
    
    public string PlaylistId { get; set; }
    public DbPlaylist Playlist { get; set; }
}