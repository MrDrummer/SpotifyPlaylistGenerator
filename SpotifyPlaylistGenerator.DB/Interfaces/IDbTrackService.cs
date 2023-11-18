using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbTrackService : IBaseTrackService
{
    public Task AddTrack(DbTrack track);
    public Task AddTracks(IEnumerable<DbTrack> tracks);
}