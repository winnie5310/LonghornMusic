using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LonghornMusic.Models;

namespace LonghornMusic.ViewModels
{
    public class SongIndexData
    {
        public IEnumerable<Song> Songs { get; set; }
        public IEnumerable<Artist> Artists { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}