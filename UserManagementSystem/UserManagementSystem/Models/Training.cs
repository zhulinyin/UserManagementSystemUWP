using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Training
    {
        public Training(string tid, string ename, string twname, string bdate, string edate)
        {
            Tid = tid;
            Ename = ename;
            TWname = twname;
            Bdate = bdate;
            Edate = edate;
        }
        public string Tid { get; set; }
        public string Ename { get; set; }
        public string TWname { get; set; }
        public string Bdate { get; set; }
        public string Edate { get; set; }
    }
}
