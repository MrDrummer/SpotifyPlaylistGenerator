using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IPlaylistService
{
    Task<IEnumerable<Playlist>> GetUserPlaylists(string userId);
    Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistsChangeMeta(IEnumerable<Playlist> playlists);
    Task<(int TrackCount, string SnapshotId)> GetPlaylistChangeMeta(Playlist playlist);
}