using SpotifyPlaylistGenerator.DB.Extensions;
using SpotifyPlaylistGenerator.DB.Interfaces;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB.Services;

public class DbArtistService : IDbArtistService
{
    private readonly SpotifyDbContext _context;

    public DbArtistService(SpotifyDbContext context)
    {
        _context = context;
    }

    public async Task AddArtist(DbArtist artist)
    {
        await _context.Artists.AddIfNotExistsAsync(artist, a => a.Id == artist.Id);
        await _context.SaveChangesAsync();
    }

    public async Task AddArtists(IEnumerable<DbArtist> artists)
    {
        await _context.Artists.AddIfNotExistsRangeAsync(artists.DistinctBy(a => a.Id), entity => e => e.Id == entity.Id);
        await _context.SaveChangesAsync();
    }
}