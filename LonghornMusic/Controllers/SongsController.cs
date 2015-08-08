using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LonghornMusic.DAL;
using LonghornMusic.Models;
using LonghornMusic.ViewModels;
using LonghornMusic.Utilities;
using System.ComponentModel.DataAnnotations;


namespace LonghornMusic.Controllers
{
       
    public class SongsController : Controller
    {
        private StoreContext db = new StoreContext();

        // GET: Songs
        public ActionResult Index()
        {
            return View(db.Song.ToList());
        }

        // GET: Songs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // GET: Songs/Create
        public ActionResult Create()
        {
            SongCreateViewModel newSong = new SongCreateViewModel();
            ViewBag.AllGenres = UpdateGenres.GetAllGenres(db);
            ViewBag.AllArtists = UpdateArtists.GetAllArtists(db);
            return View(newSong);
        }

        // POST: Songs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SongID,Name,SelectedGenres,SelectedArtist")] SongCreateViewModel newSong )
        {
                     
            if (ModelState.IsValid)
            {
                //convert integers to actual genres
                newSong.SongGenres = UpdateGenres.GetGenresFromIntList(db, newSong.SelectedGenres);

                //add artest based on id
                newSong.Artist = db.Artist.FirstOrDefault(a => a.ArtistID == newSong.SelectedArtist);
                
                //create a song based on the view model
                Song songToAdd = newSong.ToDomainModel();

                //create a validation context to validate the song
                ValidationContext vcx = new ValidationContext(songToAdd);

                //validate the song follows all the rules
                IEnumerable<ValidationResult> validResults = songToAdd.Validate(vcx);

                //if there aren't any validation results, the song is okay
                if (validResults.Count() == 0)
                {
                    db.Song.Add(songToAdd);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else  //there are validation results so we need to add them to the view model errors
                {
                    foreach (ValidationResult vResult in validResults)
                    {
                        ModelState.AddModelError("", vResult.ErrorMessage);
                    }
                }
            }
                
            //this code fires when there is something wrong, so we need to repopulate the all genres box
            ViewBag.AllGenres = UpdateGenres.GetAllGenres(db);
            ViewBag.AllArtists = UpdateArtists.GetAllArtists(db);

            //return the view model
            return View(newSong);
        }

        // GET: Songs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }

            SongCreateViewModel songToView = song.ToViewModel();
            ViewBag.AllGenres = UpdateGenres.GetAllGenres(db);
            ViewBag.AllArtists = UpdateArtists.GetAllArtists(db);
            return View(songToView);
        }

        // POST: Songs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SongID,Name,SelectedGenres,SelectedArtist")] SongCreateViewModel editedSongViewModel)
        {
            if (ModelState.IsValid) //checks the state of the view model
            {
                //Connect edited song to original song
                Song originalSong = db.Song.Find(editedSongViewModel.SongID);
                
                //Update the genre list - connecting objects has to be done manually, CurrentValues.SetValues doesn't work on objects
                UpdateGenres.AddOrUpdateSongGenre(db, originalSong, editedSongViewModel.SelectedGenres);
                UpdateArtists.AddOrUpdateArtist(db, originalSong, db.Artist.FirstOrDefault(a => a.ArtistID == editedSongViewModel.SelectedArtist));
                db.Entry(originalSong).CurrentValues.SetValues(editedSongViewModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.AllGenres = UpdateGenres.GetAllGenres(db);
            ViewBag.AllArtists = UpdateArtists.GetAllArtists(db);
            return View(editedSongViewModel);
        }

        // GET: Songs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Song song = db.Song.Find(id);
            if (song == null)
            {
                return HttpNotFound();
            }
            return View(song);
        }

        // POST: Songs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Song song = db.Song.Find(id);
            db.Song.Remove(song);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        //public ActionResult Search()//no search parameters given
        //{
        //    return View(db.Song.ToList());
        //}
        
        public ActionResult Search(string searchTitle, SearchTypes? searchType, int? searchArtist, bool? ORSearch)
        {
            var songs = from s in db.Song
                        select s;

            //songs.Include(s => s.Artist);
            if (ORSearch == false) //this is an AND search
            {
                if (String.IsNullOrEmpty(searchTitle) == false)
                {
                    if (searchType == SearchTypes.KEYWORD)
                    {
                        songs = songs.Where(s => s.Name.Contains(searchTitle));
                    }

                    else
                    {
                        songs = songs.Where(s => s.Name == searchTitle);
                    }
                }

                if (searchArtist != null && searchArtist != -1) //there is something to search for in artist
                {
                    songs = songs.Where(s => s.Artist.ArtistID == searchArtist);
                }
            }
            else if (ORSearch == true) //this is an or search
            {
                if (searchType == SearchTypes.KEYWORD)
                { 
                    songs = songs.Where(s => s.Name.Contains(searchTitle) || s.Artist.ArtistID == searchArtist);
                }
                else if (searchType == SearchTypes.EXACT)
                {
                    songs = songs.Where(s => s.Name == searchTitle || s.Artist.ArtistID == searchArtist);
                }

            }
            





            ViewBag.AllArtists = UpdateArtists.GetAllArtistsWithAll(db);
            return View(songs.ToList());
        }

      


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
