using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbPlaylistService : IDbPlaylistService
{
    private readonly SpotifyDbContext _context;

    public DbPlaylistService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public async Task<int> GetUserPlaylistCount(string userId)
    {
        var count = await _context.AppUserPlaylists
            .CountAsync(aup => aup.AppUserId == userId);

        return count;
    }

    public async Task<IEnumerable<Playlist>> GetUserPlaylists(string userId)
    {
        var playlists = await _context.Playlists
            .Where(p => p.AssociatedAppUsers.Any(aup => aup.AppUserId == userId))
            .Select(p => new 
            {
                Playlist = p,
                TrackCount = p.AssociatedTracks.Count()
            })
            .ToListAsync();

        return playlists.Select(p => p.Playlist.ToPlaylist(p.TrackCount));
    }

    public async Task AddPlaylist(DbPlaylist playlist)
    {
        await _context.Playlists.AddIfNotExistsAsync(playlist, p => p.Id == playlist.Id);
        await _context.SaveChangesAsync();
    }
    
    public async Task AddPlaylists(IEnumerable<DbPlaylist> playlists)
    {
        await _context.Playlists.AddIfNotExistsRangeAsync(playlists.DistinctBy(p => p.Id), entity => e => e.Id == entity.Id);
        await _context.SaveChangesAsync();
    }

    public async Task<Dictionary<string, (int TrackCount, string SnapshotId)>> GetPlaylistsChangeMeta(IEnumerable<Playlist> playlists)
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

    public async Task<(int TrackCount, string SnapshotId)> GetPlaylistChangeMeta(Playlist playlist)
    {
        var playlistId = playlist.Id;

        var playlistData = await _context.Playlists
            .Include(p => p.AssociatedTracks)
            .Where(p => p.Id == playlistId)
            .Select(p => new { p.AssociatedTracks.Count, p.SnapshotId })
            .FirstOrDefaultAsync();

        return (playlistData?.Count ?? 0, playlistData?.SnapshotId);
    }

    public async Task RemovePlaylistTracks(string playlistId)
    {
        
        var tracksToRemove = _context.PlaylistTracks.Where(track => track.PlaylistId == playlistId);
        
        _context.PlaylistTracks.RemoveRange(tracksToRemove);

        await _context.SaveChangesAsync();
    }
}