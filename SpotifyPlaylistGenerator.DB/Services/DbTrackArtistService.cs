using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbTrackArtistService : IDbTrackArtistService
{
    private readonly SpotifyDbContext _context;
    
    public DbTrackArtistService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddTrackArtist(DbTrackArtist trackArtist)
    {

        await _context.ArtistTracks.AddIfNotExistsAsync(trackArtist, entity =>
            entity.ArtistId == trackArtist.ArtistId &&
            entity.TrackId == trackArtist.TrackId
        );
        await _context.SaveChangesAsync();
    }

    public async Task AddTrackArtists(IEnumerable<DbTrackArtist> trackArtists)
    {
        // DistinctBy should theoretically not be needed since the position will now make it unique.
        // .DistinctBy(pt => new { pt.PlaylistId, pt.TrackId, pt.PlaylistPosition })
        await _context.ArtistTracks.AddIfNotExistsRangeAsync(trackArtists.DistinctBy(ta => new { ta.TrackId, ta.ArtistId }), entity =>
                e =>
                    e.ArtistId == entity.ArtistId &&
                    e.TrackId == entity.TrackId
                );
        await _context.SaveChangesAsync();
    }
}