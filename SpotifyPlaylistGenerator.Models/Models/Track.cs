namespace SpotifyPlaylistGenerator.Models.Models;

public class Track
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public bool Explicit { get; set; }
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
    
    public Album? Album { get; set; }
    
    // Only present when in the context of a playlist.
    // public PlaylistTrack? PlaylistTrack { get; set; }
    
    public IEnumerable<string>? PlaylistIds { get; set; }
    public IEnumerable<Playlist>? Playlists { get; set; }
    
    public IEnumerable<string>? ArtistIds { get; set; }
    public IEnumerable<Artist>? Artists { get; set; }
    
}