using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using LonghornMusic.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LonghornMusic.DAL
{
    public class StoreContext : DbContext
    {
        public StoreContext()
            : base("StoreContext") { }
        
        public DbSet<Album> Album {get; set;}
        public DbSet<Artist> Artist {get; set;}
        public DbSet<Genre> Genre {get; set;}
        public DbSet<Song> Song {get; set;}
        public DbSet<SongAlbum> SongAlbum {get; set;}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artist>().HasMany(a => a.Genres).WithMany(g => g.Artists).Map(t => t.MapLeftKey("ArtistID").MapRightKey("GenreID").ToTable("ArtistGenre"));
            modelBuilder.Entity<Song>().HasMany(s => s.Genres).WithMany(g => g.Songs).Map(t => t.MapLeftKey("SongID").MapRightKey("GenreID").ToTable("SongGenre"));
                                
        }
        
    }


}