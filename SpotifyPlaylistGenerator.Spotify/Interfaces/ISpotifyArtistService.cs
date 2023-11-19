
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Interfaces;

public interface ISpotifyArtistService // : IBaseArtistService
{
    public Task<Artist> GetFullArtist(string artistId);

    public Task<IEnumerable<Artist>> GetFullArtists(IEnumerable<string> artistIds);
}