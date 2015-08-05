using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class Album
    {
        public int AlbumId { get; set; }

        [Required]
        public string AlbumName { get; set; }

        [Required]
        public virtual Artist Artist { get; set; }
        
        public List<Song> Songs { get; set; }

        public List<Genre> Genres { get; set; }
    }
}