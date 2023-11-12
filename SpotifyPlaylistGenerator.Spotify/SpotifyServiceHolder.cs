using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace SpotifyPlaylistGenerator.Spotify;

public interface ISpotifyServiceHolder
{
    Task<SpotifyClient> GetClientAsync();
    IAPIConnector GetApiConnector();
}

public class SpotifyServiceHolder : ISpotifyServiceHolder
{
    private Task<SpotifyClient> _clientTask;
    private IAPIConnector _apiConnector;

    public SpotifyServiceHolder(SpotifyClientBuilder spotifyBuilder)
    {
        _clientTask = spotifyBuilder.BuildClient();
        _apiConnector = spotifyBuilder.ApiConnector;
    }

    public Task<SpotifyClient> GetClientAsync() => _clientTask;
    public IAPIConnector GetApiConnector() => _apiConnector;
}