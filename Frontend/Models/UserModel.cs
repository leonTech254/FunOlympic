using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Frontend.Models
{
    public class UserModel
    {
        public string user_id {get;set;}
        public string Email { get; set; }
		public string Firstname { get; set; }
		public string Password { get; set; }
		public string username {  get; set; }
        public string userRole {get;set;}
        public string Gender {get;set;}
        public string DOB {get;set;}
        public string Role {get;set;}
    }
}