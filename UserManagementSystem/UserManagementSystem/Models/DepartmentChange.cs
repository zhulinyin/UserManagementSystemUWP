using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class DepartmentChange
    {
        public DepartmentChange(string cid, string ename, string cwname, string cdate, string tname, string fname)
        {
            Cid = cid;
            Ename = ename;
            CWname = cwname;
            Cdate = cdate;
            Tname = tname;
            Fname = fname;
        }
        public string Cid { get; set; }
        public string Ename { get; set; }
        public string CWname { get; set; }
        public string Cdate { get; set; }
        public string Tname { get; set; }
        public string Fname { get; set; }
    }
}
