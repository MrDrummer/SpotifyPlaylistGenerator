namespace SpotifyPlaylistGenerator.Models.Models;

public class Playlist
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Image { get; set; }
    public string SnapshotId { get; set; }
    public bool Public { get; set; }
    public int TrackCount { get; set; }
    
    // TODO: ADD THIS BACK IN! Not sure how to map this across without the base Playlist model also containing the Owner object.
    // public string OwnerId { get; set; }
}