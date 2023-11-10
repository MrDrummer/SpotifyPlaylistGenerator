using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbPlaylistTrack
{
    [Key]
    public int Id { get; set; }
    
    public string PlaylistId { get; set; }
    public DbPlaylist Playlist { get; set; }
    
    public string TrackId { get; set; }
    public DbTrack Track { get; set; }
    
    // public int PlaylistPosition { get; set; }
    
    public DateTime AddedAt { get; set; }
}