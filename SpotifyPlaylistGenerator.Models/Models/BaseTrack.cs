namespace SpotifyPlaylistGenerator.Models.Models;

public class BaseTrack
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public bool Explicit { get; set; }
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
    
    public string AlbumId { get; set; }
    public Album? Album { get; set; }
    
    public IEnumerable<string>? ArtistIds { get; set; }
    public IEnumerable<Artist>? Artists { get; set; }
}