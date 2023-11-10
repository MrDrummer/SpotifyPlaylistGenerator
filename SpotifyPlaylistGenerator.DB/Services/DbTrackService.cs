using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbTrackService : IDbTrackService
{
    private readonly SpotifyDbContext _context;
    
    public DbTrackService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public Task<int> GetTrackCount()
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Track>> GetPlaylistTracks(string playlistId)
    {
        throw new NotImplementedException();
    }

    public async Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistChangeMeta(IEnumerable<Playlist> playlists)
    {
        var playlistIds = playlists.Select(p => p.Id);
        
        return await _context.Playlists
            .Include(p => p.AssociatedTracks)
            .Where(p => playlistIds.Contains(p.Id))
            .ToDictionaryAsync(
                p => p.Id, 
                p => (p.AssociatedTracks.Count, p.SnapshotId)
            );
    }
}