using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbPlaylist
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    // public int Followers { get; set; }
    public string Image { get; set; }
    public string SnapshotId { get; set; }
    public bool Public { get; set; }
    
    public ICollection<DbPlaylistTrack> AssociatedTracks { get; set; }
    public ICollection<DbAppUserPlaylist> AssociatedAppUsers { get; set; }
    
    // public string OwnerId { get; set; }
    // public DbUser Owner { get; set; }
}