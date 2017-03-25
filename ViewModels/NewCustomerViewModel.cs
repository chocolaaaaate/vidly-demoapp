using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Models;

namespace Vidly.ViewModels
{
    // need this because we need both Customer and MembershipType in the Customer create new View
    public class NewCustomerViewModel
    {
        //list of membership types for the dropdown box in the New Customer view 
        //using IEnumerable instead of List as keeping coupling minimal 
        public IEnumerable<MembershipType> MembershipTypes { get; set; }

        //reuse domain objects when possible to reduce duplication of code 
        //if you're finding that you're polluting the domain model too much just for the view's sake then move that stuff into a viewmodel instead to keep the metaphors in the domain pure
        public Customer Customer { get; set; }
    }
}