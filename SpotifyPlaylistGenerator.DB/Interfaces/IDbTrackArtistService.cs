using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Interfaces;

namespace SpotifyPlaylistGenerator.DB.Interfaces;

public interface IDbTrackArtistService
{
    public Task AddTrackArtist(DbTrackArtist trackArtist);
    public Task AddTrackArtists(IEnumerable<DbTrackArtist> trackArtists);
}