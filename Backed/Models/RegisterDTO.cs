using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Backed.Models
{
    public class RegisterDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public string country {get;set;}
    }
}