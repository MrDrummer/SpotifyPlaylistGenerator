using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IBaseTrackService : ITrackService
{
    Task<int> GetTrackCount();
}