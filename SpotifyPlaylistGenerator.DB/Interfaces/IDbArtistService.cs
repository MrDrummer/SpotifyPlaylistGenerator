using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbArtistService
{
    public Task AddArtist(DbArtist artist);
    public Task AddArtists(IEnumerable<DbArtist> artists);
}