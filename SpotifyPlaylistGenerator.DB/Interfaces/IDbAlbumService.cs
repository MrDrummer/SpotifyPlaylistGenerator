using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbAlbumService
{
    public Task AddAlbum(DbAlbum album);
    public Task AddAlbums(IEnumerable<DbAlbum> albums);
}