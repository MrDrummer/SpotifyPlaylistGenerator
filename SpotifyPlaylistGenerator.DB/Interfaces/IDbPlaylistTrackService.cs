using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbPlaylistTrackService : IBasePlaylistTrackService
{
    public Task AddPlaylistTrack(DbPlaylistTrack playlistTrack);
    public Task AddPlaylistTracks(IEnumerable<DbPlaylistTrack> playlistTracks);
    public Task RemovePlaylistTracks(string playlistId);
}