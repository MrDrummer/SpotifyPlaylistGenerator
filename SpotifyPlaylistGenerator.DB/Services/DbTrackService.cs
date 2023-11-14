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
    
    public Task<int> GetPlaylistTrackCount(string playlistId)
    {
        throw new NotImplementedException();
    }

    public Task GetPlaylistTracksBasicMeta(string playlistId)
    {
        // TODO: USE AddOrUpdate
        throw new NotImplementedException();
    }

    public async Task AddPlaylistTrack((DbPlaylistTrack, DbTrack) playlistTrackData)
    {
        var (playlistTrack, track) = playlistTrackData;

        await AddTrack(track);
        
        await _context.PlaylistTracks.AddIfNotExistsAsync(playlistTrack, entity =>
            entity.PlaylistId == playlistTrack.PlaylistId &&
            entity.TrackId == playlistTrack.TrackId &&
            entity.PlaylistPosition == playlistTrack.PlaylistPosition
        );
        await _context.SaveChangesAsync();
    }

    public async Task AddPlaylistTracks(IEnumerable<(DbPlaylistTrack, DbTrack)> playlistTracksData)
    {
        var tracks = playlistTracksData.Select(t => t.Item2);
        var playlistTracks = playlistTracksData.Select(t => t.Item1);

        await AddTracks(tracks);
        
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
}