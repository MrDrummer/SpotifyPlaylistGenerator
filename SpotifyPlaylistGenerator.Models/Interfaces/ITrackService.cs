using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface ITrackService
{
    Task GetPlaylistTracksBasicMeta(string playlistId);
}