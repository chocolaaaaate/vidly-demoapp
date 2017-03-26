using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        //data annotations allow us to override default conventions
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        //date of birth
        public DateTime DoB { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // a navigation property, essentially a linkage. 
        public MembershipType MembershipType { get; set; }

        /* this allows membership type to be linked without needing the entire object tagging along - this is for optimization
         this is essentially the foreign key idea of plain old RDB
         due to its name, EF will automatically treat this as a foreign key!*/
        // implicitly required as type is byte. If it was nullable byte (i.e. 'byte?') then it wouldn't be. 
        public byte MembershipTypeId { get; set; }
    }
}

