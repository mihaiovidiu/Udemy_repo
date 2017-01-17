using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
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

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/1
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                return customer;
            }
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }
        
        // POST /api/customers
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (ModelState.IsValid)
            {
                _context.Customers.Add(customer);
                _context.SaveChanges();
                return customer;
            }
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // PUT /api/customers/i
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (ModelState.IsValid)
            {
                var customerInDb = _context.Customers.SingleOrDefault(cust => cust.Id == id);
                if (customerInDb != null)
                {
                    customerInDb.Name = customer.Name;
                    customerInDb.Birthdate = customer.Birthdate;
                    customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
                    customerInDb.MembershipType = customer.MembershipType;

                    _context.SaveChanges();
                }
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else
                throw new HttpResponseException(HttpStatusCode.BadRequest);
        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(cust => cust.Id == id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
            else
                throw new HttpResponseException(HttpStatusCode.NotFound);
        }

    }
}
