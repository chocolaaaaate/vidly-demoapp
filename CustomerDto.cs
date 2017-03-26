﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime DoB { get; set; }

        public bool IsSubscribedToNewsletter { get; set; }

        // excluded because you don't want DTOs referencing domain models. Could use a MembershipTypeDto instead. 
        //public MembershipType MembershipType { get; set; }

        public byte MembershipTypeId { get; set; }
    }
}