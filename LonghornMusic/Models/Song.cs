using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class Song : IValidatableObject
    {
        [Required]
        public int SongID { get; set; }

        [Required]
        public string Name { get; set; }


        public virtual Artist Artist { get; set; }
        public virtual List<Genre> Genres { get; set; }

        public Song() 
        {
            Genres = new List<Genre>();
        }

        public Song(string inputName, List<Genre> SongGenres)
        {
            Name = inputName;
            Genres = SongGenres;
        }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Genres.Count() <= 0)
            {
                yield return new ValidationResult("Every song must have at least one genre!", new[] { "Genres" });
            }

        }
    }
}