using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Interfaces;

public interface ISpotifyTrackService : IBaseTrackService
{
    // Get Tracks and the basic metadata like Album, Artist and Genre.
    // Returns a list of Basic Tracks, unique basic Albums, unique basic Artists and UniqueGenres
    Task<PlaylistTracksBasicMeta> GetPlaylistTracksBasicMeta(string playlistId);
}