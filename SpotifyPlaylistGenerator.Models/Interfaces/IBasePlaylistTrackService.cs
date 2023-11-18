
namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IBasePlaylistTrackService : IPlaylistTrackService
{
    Task<int> GetPlaylistTrackCount(string playlistId);
}