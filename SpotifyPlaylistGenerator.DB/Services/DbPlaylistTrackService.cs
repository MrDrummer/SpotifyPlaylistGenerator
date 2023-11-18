using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbPlaylistTrackService : IDbPlaylistTrackService
{
    private readonly SpotifyDbContext _context;
    
    public DbPlaylistTrackService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public Task<int> GetPlaylistTrackCount(string playlistId)
    {
        throw new NotImplementedException();
    }

    public async Task AddPlaylistTrack(DbPlaylistTrack playlistTrack)
    {

        await _context.PlaylistTracks.AddIfNotExistsAsync(playlistTrack, entity =>
            entity.PlaylistId == playlistTrack.PlaylistId &&
            entity.TrackId == playlistTrack.TrackId &&
            entity.PlaylistPosition == playlistTrack.PlaylistPosition
        );
        await _context.SaveChangesAsync();
    }

    public async Task AddPlaylistTracks(IEnumerable<DbPlaylistTrack> playlistTracks)
    {
        // DistinctBy should theoretically not be needed since the position will now make it unique.
        // .DistinctBy(pt => new { pt.PlaylistId, pt.TrackId, pt.PlaylistPosition })
        await _context.PlaylistTracks.AddIfNotExistsRangeAsync(playlistTracks, entity =>
                e =>
                    e.PlaylistId == entity.PlaylistId &&
                    e.TrackId == entity.TrackId &&
                    e.PlaylistPosition == entity.PlaylistPosition
                );
        await _context.SaveChangesAsync();
    }

    public Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        throw new NotImplementedException();
    }

    public async Task RemovePlaylistTracks(string playlistId)
    {
        
        var tracksToRemove = _context.PlaylistTracks.Where(track => track.PlaylistId == playlistId);
        
        _context.PlaylistTracks.RemoveRange(tracksToRemove);

        await _context.SaveChangesAsync();
    }
}