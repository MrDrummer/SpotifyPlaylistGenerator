using SpotifyPlaylistGenerator.Models.Models;
using SpotifyPlaylistGenerator.Spotify.Interfaces;

namespace SpotifyPlaylistGenerator.Spotify.Services;

public class SpotifyTrackService : ISpotifyTrackService
{
    private readonly ISpotifyServiceHolder _spotifyServiceHolder;

    public SpotifyTrackService(ISpotifyServiceHolder spotifyServiceHolder)
    {
        _spotifyServiceHolder = spotifyServiceHolder;
    }
    
    public async Task<int> GetPlaylistTrackCount(string playlistId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        
        var playlist = await client.Playlists.Get(playlistId);

        return playlist.Tracks?.Total ?? 0;
    }
    
    public async Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId)
    {
        var client = await _spotifyServiceHolder.GetClientAsync();
        throw new NotImplementedException();
        
        /*
         * 1. Recursive fetch Playlist Tracks
         * 2. Figure out where Track Detail comes in
         * 3. Recursive fetch Track Album
         * 4. Recursive fetch Track Artist
         * 5. Handle Album Genres
         * 6. Handle Artist Genres
         */
        
        
    }
}