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
        List<Movie> _movies = new List<Movie>()
        {
            new Movie() { Name = "Shrek" },
            new Movie() { Name = "Wall-e" }
        };



        // GET: Movies/Random
        // Returns a random movie
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shreck!"};
            var customers = new List<Customer>()
            {
                new Customer() {Name = "Customer 1"},
                new Customer() {Name = "Customer 2"},
                new Customer() {Name = "Customer 3"},
                new Customer() {Name = "Customer 4"},
                new Customer() {Name = "Customer 5"},
                new Customer() {Name = "Customer 6"},
                new Customer() {Name = "Customer 7"}
            };


            /* Not recomended ways of passing data to a view
            ViewData["Movie"] = movie;
            ViewBag.Movie = movie;
            return View();
            */

            var viewModel = new RandomMovieViewModel() {Movie = movie, Customers = customers};

            return View(viewModel); // recomended way of passing data to a view

        }

        // Get: Movies/Edit
        public ActionResult Edit(int id)
        {
            return Content("id=" + id);
        }

        // Get: Movies
        public ActionResult Index()
        {
            return View(_movies);
        }

        // Get: Movies/ByReleaseDate
        // Action uses the Route attribute - the recommended way of declaring custom routes
        // The constraints here are called "atribute route constraints" (ex: range)
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }


    }
}