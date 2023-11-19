using SpotifyAPI.Web;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Spotify.Interfaces;

public interface ISpotifyTrackService : IBaseTrackService
{
    // Get Tracks and the basic metadata like Album, Artist and Genre.
    // Returns a list of Basic Tracks, unique basic Albums, unique basic Artists and UniqueGenres
    Task<PlaylistTracksBasicMeta> GetPlaylistTracksBasicMeta(string playlistId);
    public Task<Track> GetFullTrack(string trackId);
    public Task<IEnumerable<Track>> GetFullTracks(IEnumerable<string> trackIds);
    public Task<TrackAudioFeatures> GetTrackAudioFeatures(string trackId);
    public Task<IEnumerable<TrackAudioFeatures>> GetTracksAudioFeatures(IEnumerable<string> trackIds);
}