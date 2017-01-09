using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movies/Random
        // Returns a random movie
        public ActionResult Random()
        {
            var movie = new Movie() {Name = "Shreck!"};
            return View(movie);
        }
    }
}