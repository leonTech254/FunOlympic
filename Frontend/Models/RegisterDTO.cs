using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Models
{
    public class RegisterDTO
    {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string country {get;set;}
         public string Gender {get;set;}
        public string DOB {get;set;}
        public string Role {get;set;}
    }
}