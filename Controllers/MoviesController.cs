using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        private List<Movie> GetAllMovies()
        {
            return _context.Movies.Include(m => m.Genre).ToList();
        }

        public ActionResult Index()
        {
            return View(GetAllMovies());
        }

        public ActionResult Details(int id)
        {
            return View(GetAllMovies().Find(m => m.Id == id));
        }

        public ActionResult New()
        {
            // create a viewmodel with movie and list of genres 
            var mvm = new MovieViewModel()
            {
                Genres = _context.Genres.ToList(),
                Movie = new Movie()
            };

            return View("MovieForm", mvm);
        }

        public ActionResult Edit(int id)
        {
            var movieFromDb = _context.Movies.Single(m => m.Id == id);

            if(movieFromDb == null)
            {
                return HttpNotFound(); // trying to edit a movie that doesn't exist.
            }

            var mvm = new MovieViewModel()
            {
                Genres = _context.Genres.ToList(),
                Movie = movieFromDb
            };

            return View("MovieForm", mvm);
        }

        [HttpPost]
        public ActionResult Save(MovieViewModel mvm)
        {
           
            if (mvm.Movie.Id == 0)
            {
                // create new movie
                _context.Movies.Add(mvm.Movie);
            }
            else
            {
                // updating existing movie
                var movieBeforeEdit = _context.Movies.Single(m => m.Id == mvm.Movie.Id);
                movieBeforeEdit.Name = mvm.Movie.Name;
                movieBeforeEdit.GenreId = mvm.Movie.GenreId;
                movieBeforeEdit.ReleaseDate = mvm.Movie.ReleaseDate;
                movieBeforeEdit.DateAdded = mvm.Movie.DateAdded;
                movieBeforeEdit.NumberInStock = mvm.Movie.NumberInStock;

            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

    }
}

