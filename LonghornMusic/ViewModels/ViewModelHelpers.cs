using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LonghornMusic.Models;
using LonghornMusic.Utilities;

namespace LonghornMusic.ViewModels
{
    public static class ViewModelHelpers
    {
        public static SongCreateViewModel ToViewModel(this Song song)
        {
            var songCreateViewModel = new SongCreateViewModel()
            {
                Name = song.Name,
                SongID = song.SongID,
                Artist = song.Artist,
                SelectedGenres = song.Genres.Select(g => g.GenreID).ToList(),
                SelectedArtist = song.Artist.ArtistID
            };

            //return the ViewModel
            return songCreateViewModel;
        }

        public static Song ToDomainModel(this SongCreateViewModel songCreateViewModel)
        {
            var song = new Song(songCreateViewModel.Name, songCreateViewModel.SongGenres);
            song.SongID = songCreateViewModel.SongID;
            song.Artist = songCreateViewModel.Artist;
            //song.Genres = UpdateGenres.GetGenresFromIntList(songCreateViewModel.SelectedGenres);
            return song;
        }
    }
}