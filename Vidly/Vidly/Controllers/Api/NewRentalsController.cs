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
    public class NewRentalsController : ApiController
    {
        ApplicationDbContext _context = null;
        public NewRentalsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // Post api/newrentals/
        [HttpPost]
        public IHttpActionResult Rent(NewRentalDto newRental)
        {
            if (ModelState.IsValid)
            {
                // Get the current date
                DateTime now = DateTime.Today;
                // Get the customer
                Customer customer = _context.Customers.SingleOrDefault(cust => cust.Id == newRental.CustomerId);
                // Get the movies
                var movies = _context.Movies.Where(mov => newRental.MovieIds.Contains(mov.Id)).ToList();
                // List of rentals to add to database
                List<Rental> rentals = new List<Rental>(newRental.MovieIds.Count);

                foreach (Movie movie in movies)
                {
                    // For each rental decrease the NumberAvailable field on the movie
                    if (movie.NumberAvailable == 0)
                        return BadRequest("Movie is not available.");
                    movie.NumberAvailable--;
                    // Create a Rental and add to list
                    rentals.Add(new Rental { Customer = customer, DateRented = now, Movie = movie });
                }

                // Add rentals to database
                _context.Rentals.AddRange(rentals);
                _context.SaveChanges();

                return Ok();
                
            }
            else
                return BadRequest();
        }
    }
}
