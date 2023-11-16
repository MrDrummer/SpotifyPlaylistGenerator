using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbPlaylistService : IBasePlaylistService
{
    Task AddPlaylist(DbPlaylist playlist);
    Task AddPlaylists(IEnumerable<DbPlaylist> playlists);
    Task UpdatePlaylist(DbPlaylist playlist);
    Task UpdatePlaylists(IEnumerable<DbPlaylist> playlists);

    Task RemovePlaylistTracks(string playlistId);
}