namespace SpotifyPlaylistGenerator.Models.Models;

public class Track : BaseTrack
{
    
    // Only present when in the context of a playlist.
    // public PlaylistTrack? PlaylistTrack { get; set; }
    
    public IEnumerable<string>? PlaylistIds { get; set; }
    public IEnumerable<Playlist>? Playlists { get; set; }
    
}