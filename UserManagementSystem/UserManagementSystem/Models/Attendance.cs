using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Attendance
    {
        public Attendance(string aid, string ename, string awname, string bdate, string edate)
        {
            Aid = aid;
            Ename = ename;
            AWname = awname;
            Bdate = bdate;
            Edate = edate;
        }
        public string Aid { get; set; }
        public string Ename { get; set; }
        public string AWname { get; set; }
        public string Bdate { get; set; }
        public string Edate { get; set; }
    }
}
