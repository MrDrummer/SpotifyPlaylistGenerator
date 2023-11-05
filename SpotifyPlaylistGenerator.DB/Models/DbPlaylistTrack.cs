using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbPlaylistTrack
{
    [Key]
    public int PlaylistTrackId { get; set; }
    
    public string PlaylistId { get; set; }
    public DbPlaylist Playlist { get; set; }
    
    public string TrackId { get; set; }
    public DbTrack Track { get; set; }
    
    public DateTime AddedAt { get; set; }
}