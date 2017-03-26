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

        // GET /api/customers (by convention because return type is collection of Customer)
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/{id}
        public Customer GetCustomer(int id)
        {
            //id is read from URL thanks to our mapping in WebApiConfig.cs
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customer == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return customer;
        }

        // POST /api/customers 
        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if(customer.Name == null)
            {
                // TODO add more validation later
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            _context.Customers.Add(customer);
            _context.SaveChanges();
            return customer; // returning because server will assign it a ID that the client needs to know 

        }

        // PUT /api/customer/{id}
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            if (customer.Name == null)
            {
                // TODO add more validation later
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var custFromDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(custFromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            custFromDb.Name = customer.Name;
            custFromDb.DoB = customer.DoB;
            custFromDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            custFromDb.MembershipTypeId = customer.MembershipTypeId;

            _context.SaveChanges();
        }

        // DELETE /api/customer/{id}
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var custFromDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if (custFromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            _context.Customers.Remove(custFromDb);
            _context.SaveChanges();
        }


    }
}