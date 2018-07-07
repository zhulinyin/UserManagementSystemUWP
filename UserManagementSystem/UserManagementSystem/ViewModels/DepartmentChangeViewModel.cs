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
    class DepartmentChangeViewModel
    {
        private static DepartmentChangeViewModel uniqueInstance;
        public static DepartmentChangeViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DepartmentChangeViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<DepartmentChange> departmentChanges = new ObservableCollection<DepartmentChange>();
        public ObservableCollection<DepartmentChange> DepartmentChanges { get { return departmentChanges; } }

        public DepartmentChange SelectedItem;

        public DepartmentChangeViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetDepartmentChangesAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetEmployeeChange");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            string str = await GetDepartmentChangesAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string cid = jsonArray.GetObjectAt(i).GetNamedValue("cid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string cwname = jsonArray.GetObjectAt(i).GetNamedValue("name").ToString().TrimStart('\"').TrimEnd('\"');
                string cdate = jsonArray.GetObjectAt(i).GetNamedValue("cdate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                string tname = jsonArray.GetObjectAt(i).GetNamedValue("tname").ToString().TrimStart('\"').TrimEnd('\"');
                string fname = jsonArray.GetObjectAt(i).GetNamedValue("fname").ToString().TrimStart('\"').TrimEnd('\"');
                DepartmentChanges.Add(new DepartmentChange(cid, ename, cwname, cdate, tname, fname));
            }
        }

        public async Task<bool> CreateItem(string eid, string cway, string cdate, string tdid, string fdid)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"cway",cway },
                {"cdate",cdate },
                {"tdid",tdid },
                {"fdid",fdid }
            });
            var response = await App.client.PostAsync("/newEmployeeChange", content);
            var resdata = await response.Content.ReadAsStringAsync();
            if (resdata.Equals("true"))
            {
                ResolveJson();
                return true;
            }
            else return false;
        }
    }
}
