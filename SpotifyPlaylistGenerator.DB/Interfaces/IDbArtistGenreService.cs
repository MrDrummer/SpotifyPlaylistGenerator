using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbArtistGenreService
{
    public Task AddArtistGenre(DbArtistGenre artistGenre);
    public Task AddArtistGenres(IEnumerable<DbArtistGenre> artistGenres);
}