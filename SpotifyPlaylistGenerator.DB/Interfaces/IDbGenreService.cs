using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbGenreService
{
    public Task AddGenre(DbGenre genre);
    public Task AddGenres(IEnumerable<DbGenre> genres);
}