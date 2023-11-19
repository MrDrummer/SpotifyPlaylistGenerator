using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbAlbumGenreService
{
    public Task AddAlbumGenre(DbAlbumGenre albumGenre);
    public Task AddAlbumGenres(IEnumerable<DbAlbumGenre> albumGenres);
}