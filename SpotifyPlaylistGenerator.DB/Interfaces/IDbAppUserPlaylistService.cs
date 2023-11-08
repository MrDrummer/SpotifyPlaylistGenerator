using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbAppUserPlaylistService
{
    Task AddAppUserPlaylist(DbPlaylist playlist, DbAppUser appUser);
    Task AddAppUserPlaylists(IEnumerable<DbPlaylist> playlists, DbAppUser appUser);
}