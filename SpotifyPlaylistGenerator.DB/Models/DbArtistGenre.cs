﻿using System.ComponentModel.DataAnnotations;

namespace SpotifyPlaylistGenerator.DB.Models;

public class DbArtistGenre
{
    // [Key]
    // public int ArtistGenreId { get; set; }
    
    public string Name { get; set; }
    public DbGenre Genre { get; set; }
    
    public string ArtistId { get; set; }
    public DbArtist Artist { get; set; }
}