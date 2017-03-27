using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        // essentially represents the database
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        // GET api/movies
        public IHttpActionResult GetMovies()
        {
            //return Ok(_context.Movies.ToList());
            return Ok(_context.Movies.ToList().Select(Mapper.Map<Movie, MovieDto>));
        }

        // GET api/movies/{id}
        public IHttpActionResult GetMovie(int id)
        {
            var mov = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (mov == null)
            {
                return BadRequest();
            }

            return Ok(Mapper.Map<Movie, MovieDto>(mov));
        }

        // POST api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movDto)
        {
            if (movDto == null)
            {
                return BadRequest();
            }

            var mov = Mapper.Map<MovieDto, Movie>(movDto);
            var newlyCreatedMovie = _context.Movies.Add(mov);
            _context.SaveChanges();
            movDto.Id = newlyCreatedMovie.Id;
            return Created(new Uri(Request.RequestUri + "/" + newlyCreatedMovie.Id), movDto);
        }

        // PUT api/movies/5
        [HttpPut]
        public void UpdateMovie(int id, MovieDto movDto)
        {
            //TODO validation here

            var existingVersionOfParamMovFromDb = _context.Movies.SingleOrDefault(m => m.Id == movDto.Id);

            if (existingVersionOfParamMovFromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<MovieDto, Movie>(movDto, existingVersionOfParamMovFromDb);

            //existingVersionOfParamMovFromDb.Name = mov.Name;
            //existingVersionOfParamMovFromDb.GenreId = mov.GenreId;
            //existingVersionOfParamMovFromDb.DateAdded = mov.DateAdded;
            //existingVersionOfParamMovFromDb.NumberInStock = mov.NumberInStock;
            //existingVersionOfParamMovFromDb.ReleaseDate = mov.ReleaseDate;
            // TODO should I assign the genre too? 

            _context.SaveChanges();
        }

        // DELETE api/movies/{id}
        [HttpDelete]
        public void DeleteMovie(int id)
        {
            var movFromDb = _context.Movies.SingleOrDefault(m => m.Id == id);

            if (movFromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Movies.Remove(movFromDb);
            _context.SaveChanges();
        }
    }
}

