using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using UserManagementSystem.Models;
using Windows.Data.Json;

namespace UserManagementSystem.ViewModels
{
    class AttendanceViewModel
    {
        private static AttendanceViewModel uniqueInstance;
        public static AttendanceViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new AttendanceViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Attendance> attendances = new ObservableCollection<Attendance>();
        public ObservableCollection<Attendance> Attendances { get { return attendances; } }

        public Attendance SelectedItem;

        public AttendanceViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetAttendancesAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetAttendance");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            string str = await GetAttendancesAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string aid = jsonArray.GetObjectAt(i).GetNamedValue("aid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string awname = jsonArray.GetObjectAt(i).GetNamedValue("name").ToString().TrimStart('\"').TrimEnd('\"');
                string bdate = jsonArray.GetObjectAt(i).GetNamedValue("bdate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                string edate = jsonArray.GetObjectAt(i).GetNamedValue("edate").ToString().TrimStart('\"').TrimEnd('\"');
                if (edate != "null")
                {
                    edate=edate.Substring(0, 10);
                }
                Attendances.Add(new Attendance(aid, ename, awname, bdate, edate));
            }
        }
    }
}
