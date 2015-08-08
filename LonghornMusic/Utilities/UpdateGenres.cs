using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LonghornMusic.DAL;
using LonghornMusic.Models;
using System.Web.Mvc;

namespace LonghornMusic.Utilities
{
    public class UpdateGenres
    {
        public static void AddOrUpdateSongGenre(StoreContext context, string songName, string genreName)
        {
            var genre = context.Genre.SingleOrDefault(g => g.Name == genreName);
            var song = context.Song.SingleOrDefault(s => s.Name == songName);
            song.Genres.Add(genre);
        }


        public static void AddOrUpdateSongGenre(StoreContext context, Song song, IEnumerable<Genre> songGenres)
        {
            if (songGenres != null)
            {
                //drop existing song genres
                song.Genres.Clear();


                foreach (var songGenre in songGenres)
                {
                    song.Genres.Add(songGenre);
                }
            }
        }

        public static MultiSelectList GetAllGenres(StoreContext context)
        {
            var genres = from g in context.Genre
                         orderby g.Name
                         select g;

            List<Genre> allGenres = genres.ToList();
                                 

            MultiSelectList genreList = new MultiSelectList(allGenres, "GenreID", "Name");

            return genreList;

        }


        public static List<Genre> GetGenresFromIntList(StoreContext db, List<int> selectedGenres)
        {
            List<Genre> genresToAdd = new List<Genre>();
            
            foreach (int genreID in selectedGenres)
            {
                genresToAdd.Add(db.Genre.Find(genreID));
            }

            return genresToAdd;
        }

        public static void AddOrUpdateSongGenre(StoreContext context, Song song, List<int> genreIDs)
        {
            List<Genre> genres = GetGenresFromIntList(context, genreIDs);
            AddOrUpdateSongGenre(context, song, genres);
        }
    }
}