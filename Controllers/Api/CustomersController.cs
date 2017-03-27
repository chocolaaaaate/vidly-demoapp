using AutoMapper;
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
        public IHttpActionResult GetCustomers()
        {
            return Ok(_context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>));
        }
        //public IEnumerable<CustomerDto> GetCustomers()
        //{
        //    return _context.Customers.ToList().Select(Mapper.Map<Customer, CustomerDto>);
        //}

        // GET /api/customers/{id}
        public IHttpActionResult GetCustomer(int id)
        {
            //id is read from URL thanks to our mapping in WebApiConfig.cs
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            
            if(customer == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }

        // POST /api/customers 
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            //using IHttpActionResult to confirm to RESTful api conventions. This will send 201 instead of 200 denoting successful creation rather than just 200=ok.
            if(customerDto.Name == null)
            {
                // TODO add more validation later
                //throw new HttpResponseException(HttpStatusCode.BadRequest);
                return BadRequest();
            }

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();
            // the save caused an id to be assigned. we need to assign this back to the dto before returning it
            customerDto.Id = customer.Id;
            //return customerDto; // returning because server will assign it a ID that the client needs to know 
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);
            // this Created(..) call returns the 201.
        }

        // PUT /api/customer/{id}
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if (customerDto.Name == null)
            {
                // TODO add more validation later
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var custFromDb = _context.Customers.SingleOrDefault(c => c.Id == id);

            if(custFromDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            Mapper.Map<CustomerDto, Customer>(customerDto, custFromDb);
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