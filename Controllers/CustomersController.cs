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

            // including connected objects in the query. This is called "eager loading". 
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

        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();//getting from the db
            var newCustomerViewModel = new NewCustomerViewModel()
            {
                MembershipTypes = membershipTypes
            };
            return View("CustomerForm", newCustomerViewModel);
        }

        //[HttpPost]
        //public ActionResult Create(NewCustomerViewModel viewModel)
        //{
        //    //ensuring this can only be called by http POST
        //    return View();

        //}

        //ensuring this can only be called by http POST
        [HttpPost]
        public ActionResult Create(Customer customer)
        {
            //yes its actually the NewCustomerViewModel type but ASP.NET MVC is smart 
            //enough to see properties prefixed with Customer so it can do this binding 
            //straight to Customer. But understand why this happens. 

            // SAVING TO THE DATABASE
            _context.Customers.Add(customer);// not yet gone to DB
            _context.SaveChanges(); // NOW it was persisted to DB
            // note SaveChanges is an all or nothing operation - either all go to the DB or none. 

            // ok after they saved the customer it makes sense to send them to the list of all customers, i.e. a redirect. 
            return RedirectToAction("Index", "Customer"); // says go to Customers controller's Index method 

        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                return HttpNotFound(); // trying to edit a customer that doesn't exist 
            }

            // we're about to edit using the New action which needs a viewmodel so create it and then pass it 
            var viewModel = new NewCustomerViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel); // if we don't say go to the New action (of THIS controller), it'll look for a View called Edit.cshtml under Customers.
        }



    }
}