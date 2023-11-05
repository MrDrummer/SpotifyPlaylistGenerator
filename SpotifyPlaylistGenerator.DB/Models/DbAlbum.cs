using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbAlbum
{
    [Key]
    public string AlbumId { get; set; }
    
    public string Name { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string Image { get; set; }
    
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AlbumType AlbumType { get; set; }
    // public string Popularity { get; set; }
    
    public ICollection<DbTrack> Tracks { get; set; }
    public ICollection<DbAlbumGenre> AssociatedGenres { get; set; }
}