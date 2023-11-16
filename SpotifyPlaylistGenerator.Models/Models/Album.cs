using System.Text.Json.Serialization;
using SpotifyPlaylistGenerator.DB;

namespace SpotifyPlaylistGenerator.Models.Models;

public class Album
{
    public string Id { get; set; }
    
    public string Name { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string Image { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AlbumType AlbumType { get; set; }
    
    public IEnumerable<string>? TrackIds { get; set; }
    public IEnumerable<Track>? Tracks { get; set; }
    
    public IEnumerable<string>? GenreIds { get; set; }
    public IEnumerable<Genre>? Genres { get; set; }
}