using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbPlaylistConverter
{
    public static DbPlaylist ToDbPlaylist(this Playlist playlist)
    {
        return new DbPlaylist
        {
            Id = playlist.Id,
            Name = playlist.Name,
            Image = playlist.Image,
            SnapshotId = playlist.SnapshotId,
            Public = playlist.Public,
            // OwnerId = playlist.OwnerId
        };
    }

    public static Playlist ToPlaylist(this DbPlaylist dbPlaylist)
    {
        return new Playlist
        {
            Id = dbPlaylist.Id,
            Name = dbPlaylist.Name,
            Image = dbPlaylist.Image,
            SnapshotId = dbPlaylist.SnapshotId,
            Public = dbPlaylist.Public,
            // OwnerId = dbPlaylist.OwnerId
        };
    }
}