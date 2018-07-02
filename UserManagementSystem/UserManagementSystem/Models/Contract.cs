using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Contract
    {
        public Contract(string cid, string ename, string salary, string bdate, string edate)
        {
            Cid = cid;
            Ename = ename;
            Salary = salary;
            Bdate = bdate;
            Edate = edate;
        }
        public string Cid { get; set; }
        public string Ename { get; set; }
        public string Salary { get; set; }
        public string Bdate { get; set; }
        public string Edate { get; set; }
    }
}
