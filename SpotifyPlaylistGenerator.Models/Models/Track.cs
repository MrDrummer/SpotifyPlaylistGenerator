namespace SpotifyPlaylistGenerator.Models.Models;

public class Track
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public bool Explicit { get; set; }
    public int DiscNumber { get; set; }
    public int TrackNumber { get; set; }
}