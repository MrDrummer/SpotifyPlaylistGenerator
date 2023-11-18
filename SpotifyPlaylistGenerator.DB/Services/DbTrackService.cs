using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbTrackService : IDbTrackService
{
    private readonly SpotifyDbContext _context;
    
    public DbTrackService(SpotifyDbContext context)
    {
        _context = context;
    }
    public async Task AddTrack(DbTrack track)
    {
        await _context.Tracks.AddIfNotExistsAsync(track, t => t.Id == track.Id);
        await _context.SaveChangesAsync();
    }

    public async Task AddTracks(IEnumerable<DbTrack> tracks)
    {
        await _context.Tracks.AddIfNotExistsRangeAsync(tracks.DistinctBy(t => t.Id), entity => e => e.Id == entity.Id);
        await _context.SaveChangesAsync();
    }

    public Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        throw new NotImplementedException();
    }
}