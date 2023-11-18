using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public class PlaylistTracksBasicMeta
{
    public string SnapshotId { get; set; }
    public IEnumerable<Playlist> Playlists { get; set; }
    public IEnumerable<PlaylistTrack> PlaylistTracks { get; set; }
    public IEnumerable<Track> Tracks { get; set; }
    public IEnumerable<TrackArtist> TrackArtists { get; set; }
    public IEnumerable<Album> Albums { get; set; }
    public IEnumerable<Artist> Artists { get; set; }
    public IEnumerable<AlbumGenre> AlbumGenres { get; set; }
    public IEnumerable<TrackGenre> TrackGenres { get; set; }
    public IEnumerable<string> Genres { get; set; }
}