﻿using System;
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
            // No need to return the list of customers, jquery datatable plugin will make ajax request to the /api/Customers
            //var customers = _context.Customers.Include(c => c.MembershipType);
            //return View(customers);

            return View();
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
            var viewModel = new CustomerFormViewModel() { MembershipTypes = membershipTypes, Customer = new Customer() };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Customer customer)
        {
            // Check if input from user is valid
            if (!ModelState.IsValid)
            {
                // input not valid - return to the user the same page
                var membershipTypes = _context.MembershipTypes.ToList();
                var viewModel = new CustomerFormViewModel() { MembershipTypes = membershipTypes, Customer = customer };
                return View("CustomerForm", viewModel);
            }
            if (customer.Id == 0)
            {
                // New customer 
                _context.Customers.Add(customer);
            }
            else
            {
                // Edit an existing customer
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
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
                var viewModel = new CustomerFormViewModel()
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm", viewModel);
            }
        }
    }
}