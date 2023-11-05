using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbPlaylist
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string SnapshotId { get; set; }
}