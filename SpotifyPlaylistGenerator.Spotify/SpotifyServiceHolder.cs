using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace SpotifyPlaylistGenerator.Spotify;

public interface ISpotifyServiceHolder
{
    Task<SpotifyClient> GetClientAsync();
    Task<IAPIConnector> GetApiConnectorAsync();
}

public class SpotifyServiceHolder : ISpotifyServiceHolder
{
    private Task<SpotifyClient> _clientTask;
    private SpotifyClientBuilder _spotifyClientBuilder;

    public SpotifyServiceHolder(SpotifyClientBuilder spotifyClientBuilder)
    {
        _clientTask = spotifyClientBuilder.BuildClient();
        _spotifyClientBuilder = spotifyClientBuilder;
    }

    public Task<SpotifyClient> GetClientAsync() => _clientTask;
    
    public Task<IAPIConnector> GetApiConnectorAsync() => _spotifyClientBuilder.BuildApiConnector();
}