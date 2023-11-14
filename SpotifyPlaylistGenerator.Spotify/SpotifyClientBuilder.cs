using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace SpotifyPlaylistGenerator.Spotify;

public class SpotifyClientBuilder
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly SpotifyClientConfig _spotifyClientConfig;

    public SpotifyClientBuilder(IHttpContextAccessor httpContextAccessor, SpotifyClientConfig spotifyClientBuilder)
    {
        _httpContextAccessor = httpContextAccessor;
        _spotifyClientConfig = spotifyClientBuilder;
    }

    public async Task<SpotifyClient> BuildClient()
    {
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("Spotify", "access_token");

        return new SpotifyClient(_spotifyClientConfig.WithToken(token));
    }

    public async Task<IAPIConnector> BuildApiConnector()
    {
        var token = await _httpContextAccessor.HttpContext.GetTokenAsync("Spotify", "access_token");
        return _spotifyClientConfig
            .WithAuthenticator(new TokenAuthenticator(token, "Bearer"))
            .BuildAPIConnector();
    }
}