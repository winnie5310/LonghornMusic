using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LonghornMusic.Models;
using LonghornMusic.DAL;
using System.Web.Mvc;

namespace LonghornMusic.Utilities
{
    public static class UpdateArtists
    {
        public static SelectList GetAllArtists(StoreContext db)
        {
            var query = from a in db.Artist
                        orderby a.Name
                        select a;
            List<Artist> allArtists = query.ToList();

            SelectList list = new SelectList(allArtists, "ArtistID", "Name");

            return list;

        }

        public static SelectList GetAllArtistsWithAll(StoreContext db)
        {
            var query = from a in db.Artist
                        orderby a.Name
                        select a;
            List<Artist> allArtists = query.ToList();

            allArtists.Insert(0, new Artist { ArtistID = -1, Name = "All Artists" });

            SelectList list = new SelectList(allArtists, "ArtistID", "Name");

            return list;
        }

        public static void AddOrUpdateArtist(StoreContext db, Song song, Artist artistToAdd)
        { 
            if (song.Artist == artistToAdd) //new artist is the same as the original artist
            {
                //do nothing
            }
            else
            {
                song.Artist = artistToAdd;
            }

    
        
        }
    }
}