namespace SpotifyPlaylistGenerator.Models.Models;

public class Track
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int Duration { get; set; }
    public bool Explicit { get; set; }
    public int discNumber { get; set; }
    public int trackNumber { get; set; }
}