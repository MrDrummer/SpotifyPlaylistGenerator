using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Converters;

public static class DbPlaylistConverter
{
    public static DbPlaylist ToDbPlaylist(this Playlist playlist)
    {
        return new DbPlaylist
        {

        };
    }

    public static Playlist ToPlaylist(this DbPlaylist dbPlaylist)
    {
        return new Playlist
        {
            Id = dbPlaylist.PlaylistId,
            Name = dbPlaylist.Name,
            Image = dbPlaylist.Image,
            SnapshotId = dbPlaylist.SnapshotId
        };
    }
}