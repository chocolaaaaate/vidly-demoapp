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
        List<Customer> customers = new List<Customer>
        {
           new Customer(){Id = 1, Name = "Spongebob Squarepants"},
           new Customer(){Id = 2, Name = "Patrick Starr"}
        };

        public ActionResult Index()
        {
            return View(customers);
        }

        public ActionResult Details(int id)
        {            
            return View(customers.Find(c => c.Id == id));
        }
    }
}