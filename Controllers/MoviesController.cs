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

            return View("New", mvm);
        }

        [HttpPost]
        public ActionResult Save(MovieViewModel mvm)
        {
            if (mvm.Movie.Id == 0)
            {
                // create new movie
                _context.Movies.Add(mvm.Movie);
            }

            _context.SaveChanges();

            return RedirectToAction("Index", "Movies");
        }

    }
}

