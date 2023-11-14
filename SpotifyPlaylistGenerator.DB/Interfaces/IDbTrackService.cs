using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbTrackService : IBaseTrackService
{
    public Task AddPlaylistTrack((DbPlaylistTrack, DbTrack) playlistTrack);
    public Task AddPlaylistTracks(IEnumerable<(DbPlaylistTrack, DbTrack)> playlistTracks);

    public Task AddTrack(DbTrack track);
    public Task AddTracks(IEnumerable<DbTrack> tracks);
}