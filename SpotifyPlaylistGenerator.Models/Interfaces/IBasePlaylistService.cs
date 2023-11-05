using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IBasePlaylistService : IPlaylistService
{
    Task<int> GetUserPlaylistCount();
}