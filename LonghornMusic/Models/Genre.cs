using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class Genre
    {
        [Required]
        [Key]
        public int GenreID { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<Artist> Artists { get; set; }
        public List<Song> Songs { get; set; }
    }
}