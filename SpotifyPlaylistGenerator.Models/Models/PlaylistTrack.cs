namespace SpotifyPlaylistGenerator.Models.Models;

public class PlaylistTrack : Track
{
    public int Id { get; set; }
    
    public int PlaylistPosition { get; set; }
    public DateTime AddedAt { get; set; }
}