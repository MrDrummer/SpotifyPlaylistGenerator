using SpotifyAPI.Web;

namespace SpotifyPlaylistGenerator.Services;

public interface ISpotifyServiceHolder
{
    Task<SpotifyClient> GetClientAsync();
}

public class SpotifyServiceHolder : ISpotifyServiceHolder
{
    private Task<SpotifyClient> _clientTask;

    public SpotifyServiceHolder(SpotifyClientBuilder spotifyBuilder)
    {
        _clientTask = spotifyBuilder.BuildClient();
    }

    public Task<SpotifyClient> GetClientAsync() => _clientTask;
}