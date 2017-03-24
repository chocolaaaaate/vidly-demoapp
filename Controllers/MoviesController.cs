using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        public ActionResult Random()
        {
            var customers = new List<Customer>
            {
                new Customer { Name = "Customer 1"},
                new Customer { Name = "Customer 2"}
            };
            var movie = new Movie() { Name = "Nacho Libre!" };


            var viewModel = new RandomMovieViewModel
            {
                Movie = movie,
                Customers = customers
            };

            return View(viewModel);
        }

        public ActionResult Edit(int id)
        {
            return Content("There are " + id + " ducks!");
        }

        //public ActionResult Index(int? pageIndex, string sortBy)
        //{
        //    if (!pageIndex.HasValue)
        //    {
        //        pageIndex = 1;
        //    }
        //    if (String.IsNullOrWhiteSpace(sortBy))
        //    {
        //        sortBy = "Name";
        //    }
        //    return Content("Index called. pageIndex = " + pageIndex + ", sortBy = " + sortBy);
        //}

        [Route("movies/released/{year}/{month:regex(\\d{4}):range(1,12)}")] // note adding constraints to possible attribute parameter values
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + " / " + month); 
        }

        public ActionResult Index()
        {
            List<Movie> allMovies = new List<Movie>
            {
                new Movie() { Id = 1, Name = "The Eye" },
                new Movie() { Id = 2, Name = "Nacho Libre" }
            };

            return View(allMovies);
        }
        
    }
}

