using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface ITrackService
{
    Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId);
}