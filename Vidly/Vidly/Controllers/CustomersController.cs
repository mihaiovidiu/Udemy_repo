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
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        // GET: Customers
        public ActionResult Index()
        {
            var customers = _context.Customers.Include(c => c.MembershipType);
            return View(customers);
        }

        public ActionResult Details(int id)
        {
            Customer cust = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (cust != null)
                return View(cust);
            else
                return HttpNotFound();
        }

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewMode() { MembershipTypes = membershipTypes };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return HttpNotFound();
            else
            {
                var viewModel = new CustomerFormViewMode()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
        }
    }
}