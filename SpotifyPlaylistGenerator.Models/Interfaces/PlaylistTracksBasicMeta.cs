using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public class PlaylistTracksBasicMeta
{
    public IEnumerable<PlaylistTrack> PlaylistTracks { get; set; }
    public Dictionary<string, Album> UniqueAlbums { get; set; }
    public Dictionary<string, Artist> UniqueArtists { get; set; }
    public string[] UniqueGenres { get; set; }
}