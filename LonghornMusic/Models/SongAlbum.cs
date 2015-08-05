using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class SongAlbum
    {
        public int SongAlbumID { get; set; }

        [Required]
        public int TrackNumber { get; set; }

        [Required]
        public virtual Song Song { get; set; }

        [Required]
        public virtual Album Album { get; set; }
    }
}