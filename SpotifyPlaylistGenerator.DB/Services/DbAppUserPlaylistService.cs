using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbAppUserPlaylistService : IDbAppUserPlaylistService
{
    private readonly SpotifyDbContext _context;
    private readonly DbAppUser _appUser;
    private readonly DbPlaylist _playlist;

    public DbAppUserPlaylistService(SpotifyDbContext context, DbAppUser appUser, DbPlaylist playlist)
    {
        _context = context;
        _appUser = appUser;
        _playlist = playlist;
    }
    
    public async Task AddAppUserPlaylist(DbPlaylist playlist, DbAppUser appUser)
    {
        var existingPlaylist = _context.Playlists.Where(p => p.PlaylistId == playlist.PlaylistId).SingleOrDefault();
        var existingAppUser = _context.AppUsers.Where(au => au.AppUserId == appUser.AppUserId).SingleOrDefault();
        var existingJunction = _context.AppUserPlaylists.Where(aup => aup.AppUserId == appUser.AppUserId && aup.PlaylistId == playlist.PlaylistId).SingleOrDefault();

        if (existingPlaylist == null) _context.Playlists.Add(playlist);
        if (existingAppUser == null) _context.AppUsers.Add(appUser);
        if (existingJunction == null)
            _context.AppUserPlaylists.Add(new DbAppUserPlaylist
            {
                AppUserId = appUser.AppUserId,
                PlaylistId = playlist.PlaylistId
            });
        await _context.SaveChangesAsync();
    }

    public Task AddAppUserPlaylists(IEnumerable<DbPlaylist> playlists, IEnumerable<DbAppUser> appUsers)
    {
        throw new NotImplementedException();
    }
}