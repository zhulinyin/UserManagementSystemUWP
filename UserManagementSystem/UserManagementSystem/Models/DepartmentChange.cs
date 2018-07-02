using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class DepartmentChange
    {
        public DepartmentChange(string cid, string ename, string cwname, string cdate, string dname)
        {
            Cid = cid;
            Ename = ename;
            CWname = cwname;
            Cdate = cdate;
            Dname = dname;
        }
        public string Cid { get; set; }
        public string Ename { get; set; }
        public string CWname { get; set; }
        public string Cdate { get; set; }
        public string Dname { get; set; }
    }
}
