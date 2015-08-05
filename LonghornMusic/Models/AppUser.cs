using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace LonghornMusic.Models
{
    public class AppUser : IdentityUser
    {
        public string StreetAddress { get; set; }
        public string City { get; set; }

        [StringLength(2,ErrorMessage="State can only be two letters.  Please use the abbreviation.")]
        public string State { get; set; }
        
        [DataType(DataType.PostalCode)]
        public string Zip { get; set; }
        
        public bool Enabled { get; set; }
    }
}