using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        List<Customer> _customers = new List<Customer>()
            {
                new Customer() {Name = "John Smith", Id = 1 },
                new Customer() {Name = "Mary Wiliams", Id = 2 }
            };

        // here I test the no customers scenario.
        // List<Customer> _customers = new List<Customer>(); 



        // GET: Customers
        public ActionResult Index()
        {
            
            return View(_customers);
        }

        public ActionResult Details(int id)
        {
            Customer cust = _customers.FirstOrDefault(c => { return c.Id == id; });
            if (cust != null)
                return View(cust);
            else
                return HttpNotFound();
        }
    }
}