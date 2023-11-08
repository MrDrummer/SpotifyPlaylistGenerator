using Microsoft.EntityFrameworkCore;
using SpotifyPlaylistGenerator.DB.Models;

namespace SpotifyPlaylistGenerator.DB;

public class SpotifyDbContext : DbContext
{
    public DbSet<DbUser> Users { get; set; }
    public DbSet<DbPlaylist> Playlists { get; set; }
    public DbSet<DbPlaylistTrack> PlaylistTracks { get; set; }
    public DbSet<DbTrack> Tracks { get; set; }
    public DbSet<DbAlbum> Albums { get; set; }
    public DbSet<DbAlbumGenre> AlbumGenres { get; set; }
    public DbSet<DbGenre> Genres { get; set; }
    public DbSet<DbArtistTrack> ArtistTracks { get; set; }
    public DbSet<DbArtist> Artists { get; set; }
    public DbSet<DbArtistGenre> ArtistGenres { get; set; }
    public DbSet<DbAppUser> AppUsers { get; set; }
    public DbSet<DbAppUserPlaylist> AppUserPlaylists { get; set; }
    
    public SpotifyDbContext(DbContextOptions<SpotifyDbContext> options) : base(options) {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Artist-Genre relationship
        modelBuilder.Entity<DbArtistGenre>()
            .HasKey(ag => new { ag.ArtistId, ag.GenreId });

        modelBuilder.Entity<DbArtistGenre>()
            .HasOne(ag => ag.Artist)
            .WithMany(artist => artist.AssociatedGenres)
            .HasForeignKey(ag => ag.ArtistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DbArtistGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany(genre => genre.AssociatedArtists)
            .HasForeignKey(ag => ag.GenreId)
            .OnDelete(DeleteBehavior.Cascade);

        // Artist-Track relationship
        modelBuilder.Entity<DbArtistTrack>()
            .HasKey(at => new { at.ArtistId, at.TrackId });

        modelBuilder.Entity<DbArtistTrack>()
            .HasOne(at => at.Artist)
            .WithMany(artist => artist.AssociatedTracks)
            .HasForeignKey(at => at.ArtistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DbArtistTrack>()
            .HasOne(at => at.Tracks)
            .WithMany(track => track.AssociatedArtists)
            .HasForeignKey(at => at.TrackId)
            .OnDelete(DeleteBehavior.NoAction);

        // Album-Genre relationship
        modelBuilder.Entity<DbAlbumGenre>()
            .HasKey(ag => new { ag.AlbumId, ag.GenreId });

        modelBuilder.Entity<DbAlbumGenre>()
            .HasOne(ag => ag.Album)
            .WithMany(album => album.AssociatedGenres)
            .HasForeignKey(ag => ag.AlbumId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DbAlbumGenre>()
            .HasOne(ag => ag.Genre)
            .WithMany(genre => genre.AssociatedAlbums)
            .HasForeignKey(ag => ag.GenreId)
            .OnDelete(DeleteBehavior.Cascade);
        
        // Playlist-Track relationship
        modelBuilder.Entity<DbPlaylistTrack>()
            .HasKey(pt => new { pt.PlaylistId, pt.TrackId });

        modelBuilder.Entity<DbPlaylistTrack>()
            .HasOne(pt => pt.Playlist)
            .WithMany(playlist => playlist.AssociatedTracks)
            .HasForeignKey(pt => pt.PlaylistId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DbPlaylistTrack>()
            .HasOne(pt => pt.Track)
            .WithMany(track => track.AsssociatedPlaylists)
            .HasForeignKey(pt => pt.TrackId)
            .OnDelete(DeleteBehavior.NoAction);

        // Relationship between User and Playlist
        // modelBuilder.Entity<DbPlaylist>()
        //     .HasOne(p => p.Owner)
        //     .WithMany(u => u.Playlists)
        //     .HasForeignKey(p => p.OwnerId)
        //     .OnDelete(DeleteBehavior.NoAction);
        
        // AppUser-Playlist relationship
        modelBuilder.Entity<DbAppUserPlaylist>()
            .HasKey(ap => new { ap.AppUserId, ap.PlaylistId });

        modelBuilder.Entity<DbAppUserPlaylist>()
            .HasOne(ap => ap.AppUser)
            .WithMany(user => user.AssociatedPlaylists)
            .HasForeignKey(ap => ap.AppUserId)
            .OnDelete(DeleteBehavior.NoAction);

        modelBuilder.Entity<DbAppUserPlaylist>()
            .HasOne(ap => ap.Playlist)
            .WithMany(playlist => playlist.AssociatedAppUsers)
            .HasForeignKey(ap => ap.PlaylistId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}