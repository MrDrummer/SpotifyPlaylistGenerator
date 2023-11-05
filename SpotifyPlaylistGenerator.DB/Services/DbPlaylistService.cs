using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Converters;
using SpotifyPlaylistGenerator.DB.Interfaces;
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
            .ToListAsync();

        return playlists.Select(p => p.ToPlaylist());
    }
}