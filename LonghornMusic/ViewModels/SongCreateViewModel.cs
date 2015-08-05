using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LonghornMusic.Models;
using LonghornMusic.DAL;
using System.ComponentModel.DataAnnotations;


namespace LonghornMusic.ViewModels
{
    public class SongCreateViewModel //: IValidatableObject
    {
        
        public int SongID { get; set; }

        [Required]
        public string Name { get; set; }

        public Artist Artist { get; set; }

        [Required]
        public virtual List<Genre> SongGenres { get; set; }
      
        //Genres selected by the user
        private List<int> _selectedGenres { get; set; }

        [Required]
        public int SelectedArtist { get; set; }


        public SongCreateViewModel()
        {
            // add an empty list so there is an add method
            SongGenres = new List<Genre>();
        }

       

        public List<int> SelectedGenres
        { 
            get
            {
                return _selectedGenres ?? new List<int>();
            }

            set
            {
                _selectedGenres = value;
            }
        
        }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (SongGenres.Count() <= 0)
        //    {
        //        yield return new ValidationResult("Every song must have at least one genre!", new[] { "SongGenres" });
        //    }

        //}
    }
}