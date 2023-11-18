using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbTrackArtist
{
    // [Key]
    // public int ArtistTrackId { get; set; }
    
    public string ArtistId { get; set; }
    public DbArtist Artist { get; set; }
    
    public string TrackId { get; set; }
    public DbTrack Track { get; set; }
    
    public int ArtistIndex { get; set; }
}