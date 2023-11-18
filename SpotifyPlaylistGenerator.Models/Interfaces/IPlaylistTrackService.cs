namespace SpotifyPlaylistGenerator.Models.Interfaces;

public interface IPlaylistTrackService
{
    Task GetPlaylistTracksBasicMeta(string playlistId);
}