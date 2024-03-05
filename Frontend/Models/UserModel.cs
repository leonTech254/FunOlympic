using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Frontend.Models
{
    public class UserModel
    {
        public String user_id {get;set;}
        public String Email { get; set; }
        public string ProfileUrl { get; set; }
		public String Firstname { get; set; }
		public String Password { get; set; }
		public String username {  get; set; }
        public string userRole {get;set;}
        public Boolean iSAccountActive {get;set;}

        public static implicit operator UserModel?(JToken? v)
        {
            throw new NotImplementedException();
        }
    }
}