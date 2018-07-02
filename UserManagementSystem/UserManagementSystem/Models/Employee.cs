using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Employee
    {
        public Employee(string eid, string ename, string dname, string ebirth, string status, string esex, string ehometown, string ebody)
        {
            Eid = eid;
            Ename = ename;
            Dname = dname;
            Ebirth = ebirth;
            Status = status;
            Esex = esex;
            Ehometown = ehometown;
            Ebody = ebody;
        }
        public string Eid { get; set; }
        public string Ename { get; set; }
        public string Dname { get; set; }
        public string Ebirth { get; set; }
        public string Status { get; set; }
        public string Esex { get; set; }
        public string Ehometown { get; set; }
        public string Ebody { get; set; }
    }
}
