namespace SpotifyPlaylistGenerator.Models.Models;

public class TrackArtist
{
    public string ArtistId { get; set; }
    public Artist? Artist { get; set; }
    
    public string TrackId { get; set; }
    public Track? Track { get; set; }
    
    public int ArtistIndex { get; set; }
}