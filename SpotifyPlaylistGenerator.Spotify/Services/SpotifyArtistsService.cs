using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Converters;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyArtistService : ISpotifyArtistService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public SpotifyArtistService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }

    public async Task<Artist> GetFullArtist(string albumId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var album = await client.Artists.Get(albumId);

        return album.ToArtist();
    }

    public async Task<IEnumerable<Artist>> GetFullArtists(IEnumerable<string> albumIds)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();

        var albums = await client.Artists.GetSeveral(new ArtistsRequest(albumIds.ToList()));

        return albums.Artists.Select(a => a.ToArtist());
    }
}