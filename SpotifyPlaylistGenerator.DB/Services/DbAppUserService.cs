using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbAppUserService : IDbAppUserService
{
    private readonly SpotifyDbContext _context;

    public DbAppUserService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public Task AddAppUser(DbAppUser appUser)
    {
        throw new NotImplementedException();
    }
}