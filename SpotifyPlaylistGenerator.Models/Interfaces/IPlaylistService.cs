using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<Playlist>> GetUserPlaylists();
}