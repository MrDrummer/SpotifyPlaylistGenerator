using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.Models.Interfaces;
using SpotifyPlaylistGenerator.Models.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbTrackService : IDbTrackService
{
    private readonly SpotifyDbContext _context;
    
    public DbTrackService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public Task<int> GetPlaylistTrackCount(string playlistId)
    {
        throw new NotImplementedException();
    }

    public Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        // TODO: USE AddOrUpdate
        throw new NotImplementedException();
    }
}