using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbAppUserPlaylistService : IDbAppUserPlaylistService
{
    private readonly SpotifyDbContext _context;

    public DbAppUserPlaylistService(SpotifyDbContext context)
    {
        _context = context;
    }
    
    public async Task AddAppUserPlaylist(DbPlaylist playlist, DbAppUser appUser)
    {
        var existingPlaylist = _context.Playlists.SingleOrDefault(p => p.Id == playlist.Id);
        var existingAppUser = _context.AppUsers.SingleOrDefault(au => au.Id == appUser.Id);
        var existingJunction = _context.AppUserPlaylists.SingleOrDefault(aup => aup.AppUserId == appUser.Id && aup.PlaylistId == playlist.Id);

        if (existingPlaylist == null) _context.Playlists.Add(playlist);
        if (existingAppUser == null) _context.AppUsers.Add(appUser);
        if (existingJunction == null)
        {
            _context.AppUserPlaylists.Add(new DbAppUserPlaylist
            {
                AppUserId = appUser.Id,
                PlaylistId = playlist.Id
            });
        }
        await _context.SaveChangesAsync();
    }

    public async Task AddAppUserPlaylists(IEnumerable<DbPlaylist> playlists, DbAppUser appUser)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        
        var existingPlaylistIds = await _context.Playlists
            .Where(p => playlists.Select(pl => pl.Id).Contains(p.Id))
            .Select(p => p.Id)
            .ToListAsync();

        var playlistsToAdd = playlists.Where(pl => !existingPlaylistIds.Contains(pl.Id)).ToList();

        if (playlistsToAdd.Any())
        {
            await _context.Playlists.AddRangeAsync(playlistsToAdd);
            await _context.SaveChangesAsync();
        }

        var existingAppUser = await _context.AppUsers.FindAsync(appUser.Id);
        if (existingAppUser == null)
        {
            await _context.AppUsers.AddAsync(appUser);
            await _context.SaveChangesAsync();
        }

        var existingJunctions = await _context.AppUserPlaylists
            .Where(aup => aup.AppUserId == appUser.Id && playlists.Select(pl => pl.Id).Contains(aup.PlaylistId))
            .ToListAsync();

        var junctionsToAdd = playlists
            .Where(pl => existingJunctions.All(aup => aup.PlaylistId != pl.Id))
            .Select(pl => new DbAppUserPlaylist {AppUserId = appUser.Id, PlaylistId = pl.Id})
            .ToList();

        if (junctionsToAdd.Any())
        {
            await _context.AppUserPlaylists.AddRangeAsync(junctionsToAdd);
            await _context.SaveChangesAsync();
        }

        await transaction.CommitAsync();
        
        // var existingPlaylist = _context.Playlists.SingleOrDefault(p => p.Id == playlist.Id);
        // var existingAppUser = _context.AppUsers.SingleOrDefault(au => au.Id == appUser.Id);
        // var existingJunction = _context.AppUserPlaylists.SingleOrDefault(aup => aup.AppUserId == appUser.Id && aup.PlaylistId == playlist.Id);
        //
        // if (existingPlaylist == null) _context.Playlists.Add(playlist);
        // if (existingAppUser == null) _context.AppUsers.Add(appUser);
        // if (existingJunction == null)
        // {
        //     _context.AppUserPlaylists.Add(new DbAppUserPlaylist
        //     {
        //         AppUserId = appUser.Id,
        //         PlaylistId = playlist.Id
        //     });
        // }
        // await _context.SaveChangesAsync();
    }
}