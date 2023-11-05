using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbTrack
{
    [Key]
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    // Artists
    // Album (Full Track)
}