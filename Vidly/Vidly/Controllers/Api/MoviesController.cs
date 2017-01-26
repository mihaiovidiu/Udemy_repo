using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;
using System.Data.Entity;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context = null;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // Get /api/movies
        public IHttpActionResult GetMovies(string name=null)
        {
            var movieQuery = _context.Movies
                .Include(mov => mov.Genre)
                .Where(mov => mov.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(name))
                movieQuery = movieQuery.Where(mov => mov.Name.Contains(name));

            return Ok(movieQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>));
        }

        // Get /api/movies/id
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movieInDb != null)
            {
                return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
            }
            else
                return NotFound();
        }

        // Post /api/movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                var movie = Mapper.Map<MovieDto, Movie>(movieDto);

                try
                {
                    _context.Movies.Add(movie);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }

                movieDto.Id = movie.Id;
                return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
            }
            else
            {
                // Get the errors
                var errorBuilder = new System.Text.StringBuilder();
                foreach (var modelState in ModelState.Values)
                    foreach (var modelError in modelState.Errors)
                        errorBuilder.Append(modelError.ErrorMessage + "; ");

                return BadRequest(errorBuilder.ToString());

            }
        }

        // PUT /api/movies/id
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (ModelState.IsValid)
            {
                // Find the movie
                var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == id);
                if (movieInDb != null)
                {
                    // Update the movie
                    try
                    {
                        Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return InternalServerError(ex);
                    }

                    // Success
                    return Ok();
                }
                else
                    return NotFound();
            }
            else
            {
                // Get the errors
                var errorBuilder = new System.Text.StringBuilder();
                foreach (var modelState in ModelState.Values)
                    foreach (var modelError in modelState.Errors)
                        errorBuilder.Append(modelError.ErrorMessage + "; ");

                return BadRequest(errorBuilder.ToString());
            }
        }

        // DELETE /api/movies/id
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                _context.SaveChanges();

                return Ok();
            }
            else
                return NotFound();
        }
    }
}
