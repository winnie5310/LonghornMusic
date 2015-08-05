namespace LonghornMusic.StoreMigrations
{
    using LonghornMusic.DAL;
    using LonghornMusic.Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using LonghornMusic.Utilities;

    internal sealed class Configuration : DbMigrationsConfiguration<LonghornMusic.DAL.StoreContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            MigrationsDirectory = @"StoreMigrations";
        }

        protected override void Seed(LonghornMusic.DAL.StoreContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var genres = new List<Genre>
            {
                new Genre {Name = "Reggae", Description = "Music from the Carribean"},
                new Genre {Name = "Pop", Description = "Music Played on the Radio Too Much"},
                new Genre {Name = "Oldies", Description = "Music from a long time ago"},
                new Genre {Name = "Country", Description = "Music from the south"}
            };

            genres.ForEach(g => context.Genre.AddOrUpdate(x => x.Name, g));
            context.SaveChanges();

            var artists = new List<Artist>
            {
                new Artist {Name = "Bob Marley", Genres = new List<Genre>()},
                new Artist {Name = "Sam Smith", Genres = new List<Genre>()},
                new Artist {Name = "Elvis Presley", Genres = new List<Genre>()}

            };

            artists.ForEach(a => context.Artist.AddOrUpdate(b => b.Name, a));
            context.SaveChanges();

            AddOrUpdateArtistGenre(context, "Bob Marley", "Reggae");
            AddOrUpdateArtistGenre(context, "Bob Marley", "Pop");
            AddOrUpdateArtistGenre(context, "Sam Smith", "Pop");
            context.SaveChanges();

            var songs = new List<Song>
            {
                new Song {Name = "Stay With Me", Artist = context.Artist.SingleOrDefault(a => a.Name == "Sam Smith"), Genres = new List<Genre>()},
                new Song {Name = "Hound Dog", Artist = context.Artist.SingleOrDefault(a => a.Name == "Elvis Presley"), Genres = new List<Genre>()},
                new Song {Name = "Jailhouse Rock", Artist = context.Artist.SingleOrDefault(a => a.Name == "Elvis Presley"), Genres = new List<Genre>()}
            };

            songs.ForEach(s => context.Song.AddOrUpdate(b => b.Name, s));
            context.SaveChanges();

            UpdateGenres.AddOrUpdateSongGenre(context, "Stay With Me", "Pop");
            UpdateGenres.AddOrUpdateSongGenre(context, "Hound Dog", "Oldies");
            UpdateGenres.AddOrUpdateSongGenre(context, "Hound Dog", "Country");
            UpdateGenres.AddOrUpdateSongGenre(context, "Jailhouse Rock", "Oldies");
            context.SaveChanges();
        }

        void AddOrUpdateArtistGenre(StoreContext context, string artistName, string genreName)
        {
            var genre = context.Genre.SingleOrDefault( g => g.Name == genreName);

            var artist = context.Artist.SingleOrDefault(a => a.Name == artistName);
            
            artist.Genres.Add(genre);
        }

       
    }
}
