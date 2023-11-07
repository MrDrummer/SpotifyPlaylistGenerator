using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbAppUserService
{
    Task AddAppUser(DbAppUser appUser);
}