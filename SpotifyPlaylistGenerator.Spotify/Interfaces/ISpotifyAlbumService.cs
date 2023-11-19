
using System.Collections;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Interfaces;

public interface ISpotifyAlbumService // : IBaseAlbumService
{
    public Task<Album> GetFullAlbum(string albumId);

    public Task<IEnumerable<Album>> GetFullAlbums(IEnumerable<string> albumIds);
}