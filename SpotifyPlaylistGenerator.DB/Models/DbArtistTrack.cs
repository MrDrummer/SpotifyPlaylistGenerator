using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbArtistTrack
{
    // [Key]
    // public int ArtistTrackId { get; set; }
    
    public string ArtistId { get; set; }
    public DbArtist Artist { get; set; }
    
    public string TrackId { get; set; }
    public DbTrack Tracks { get; set; }
    
    public int ArtistIndex { get; set; }
}