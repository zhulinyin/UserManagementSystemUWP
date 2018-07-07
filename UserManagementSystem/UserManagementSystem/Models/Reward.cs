using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementSystem.Models
{
    class Reward
    {
        public Reward(string rid, string ename, string rreason, string rway, string rdate, string rmoney)
        {
            Rid = rid;
            Ename = ename;
            Rreason = rreason;
            Rway = rway;
            Rdate = rdate;
            Rmoney = rmoney;
        }
        public string Rid { get; set; }
        public string Ename { get; set; }
        public string Rreason { get; set; }
        public string Rway { get; set; }
        public string Rdate { get; set; }
        public string Rmoney { get; set; }
    }
}
