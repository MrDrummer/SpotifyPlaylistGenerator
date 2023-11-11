using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbTrack
{
    [Key]
    public string Id { get; set; }
    
    public string Name { get; set; }
    public int DurationMs { get; set; }
    public bool Explicit { get; set; }
    public int? DiscNumber { get; set; }
    public int? TrackNumber { get; set; }
    public int Popularity { get; set; }
    
    
    public string AlbumId { get; set; }
    public DbAlbum Album { get; set; }
    
    public ICollection<DbArtistTrack> AssociatedArtists { get; set; }
    public ICollection<DbPlaylistTrack> AsssociatedPlaylists { get; set; }
}