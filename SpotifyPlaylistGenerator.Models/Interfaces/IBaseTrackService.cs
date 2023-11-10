
namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IBaseTrackService : ITrackService
{
    Task<int> GetPlaylistTrackCount(string playlistId);
}