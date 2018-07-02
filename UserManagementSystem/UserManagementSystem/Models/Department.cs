using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Department
    {
        public Department(string did, string dname, string ename)
        {
            Did = did;
            Dname = dname;
            Ename = ename;
        }
        public string Did { get; set; }
        public string Dname { get; set; }
        public string Ename { get; set; }
    }
}
