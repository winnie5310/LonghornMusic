using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class Artist
    {
        [Required]
        public int ArtistID { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual List<Genre> Genres { get; set; }

        public virtual List<Song> Songs { get; set; }
    }
}