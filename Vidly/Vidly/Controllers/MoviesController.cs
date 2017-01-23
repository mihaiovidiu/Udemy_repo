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
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // Get: Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");

        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var allTheGenres = _context.Genres.ToList();
            var viewModel = new MovieFormViewModel(allTheGenres);
            return View("MovieForm", viewModel);
        }

        public ActionResult Edit(int id)
        {
            var allTheGenres = _context.Genres.ToList();
            // Get movie from db
            var movie = _context.Movies.Find(id);
            if (movie != null)
            {
                var viewModel = new MovieFormViewModel(movie, allTheGenres);
                return View("MovieForm", viewModel);
            }
            else
                return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie, _context.Genres.ToList());
                return View("MovieForm", viewModel);
            }

            // Data posted by the user is valid
            movie.DateAdded = DateTime.Today;
            if (movie.Id == 0)
            {
                // Add movie to database
                _context.Movies.Add(movie);
            }
            else
            {
                // Get movie from db
                var movieFromDb = _context.Movies.Find(movie.Id);
                // Edit movie
                movieFromDb.GenreId = movie.GenreId;
                movieFromDb.Name = movie.Name;
                movieFromDb.NumberInStock = movie.NumberInStock;
                movieFromDb.ReleaseDate = movie.ReleaseDate;
            }
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

#region Testing end experimenting part

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



        // Get: Movies/ByReleaseDate
        // Action uses the Route attribute - the recommended way of declaring custom routes
        // The constraints here are called "atribute route constraints" (ex: range)
        [Route("movies/released/{year:regex(\\d{4})}/{month:regex(\\d{2}):range(1,12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content(year + "/" + month);
        }

        public ActionResult Details(int id)
        {
            Movie m = _context.Movies.Include("Genre").FirstOrDefault(mov => mov.Id == id);
            if (m != null)
                return View(m);
            else
                return HttpNotFound();
        }

        #endregion


    }
}