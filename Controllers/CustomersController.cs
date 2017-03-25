using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {

        /* TO get data from db, need a DbContext (it represents the DB)*/
        private ApplicationDbContext _context;

        public CustomersController()
        {
            // ApplicationDbContext was defined in IndentityModels. 
            _context = new ApplicationDbContext();
            // this is a disposable object so we need to dispose it properly
        }

        //disposing the dbcontext 
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            base.Dispose(disposing);
        }

        private List<Customer> GetAllCustomers()
        {
            // when only loading the customer objects not their associated membershiptype objects 
            // var allCustomers = _context.Customers.ToList();


            var allCustomers = _context.Customers.Include(c => c.MembershipType).ToList();

            return allCustomers;
        }

        public ActionResult Index()
        {
            return View(GetAllCustomers());
        }

        public ActionResult Details(int id)
        {
            // could also use .SingleOrDefault instead of Find
            return View(GetAllCustomers().Find(c => c.Id == id));
        }
    }
}