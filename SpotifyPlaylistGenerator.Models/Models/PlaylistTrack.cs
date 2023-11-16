namespace SpotifyPlaylistGenerator.Models.Models;

public class PlaylistTrack : BaseTrack
{
    // Extends Track, so doesn't need the PlaylistTrack or TrackId.
    // public int Id { get; set; }
    
    public string TrackId { get; set; }
    public string PlaylistId { get; set; }
    public Playlist Playlist { get; set; }
    
    public int PlaylistIndex { get; set; }
    public DateTime AddedAt { get; set; }
}