using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class User
    {
        public User(string username, string uright)
        {
            Username = username;
            Uright = uright;
        }
        public string Username { get; set; }
        public string Uright { get; set; }
    }
}
