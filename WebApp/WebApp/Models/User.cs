using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public String Name { get; set; }
        public String Last_Name { get; set; }
        public String Phone { get; set; }
       
        public String Email { get; set; }
        public String Password { get; set; }
        public String Type_User { get; set; }

     
    }
}
