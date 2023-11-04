using SpotifyAPI.Web;

namespace SpotifyPlaylistGenerator.Services;

public class PlaylistService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public PlaylistService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }

    public async Task<List<SimplePlaylist>> GetCurrentUserPlaylists()
    {
        var client = await _spotifyServiceHolder.GetClientAsync();


        var firstPlaylistPage = await client.Playlists.CurrentUsers();

        var playlists = (await client.PaginateAll(firstPlaylistPage)).ToList();

        return playlists;
    }
}