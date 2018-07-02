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
                string cdate = jsonArray.GetObjectAt(i).GetNamedValue("cdate").ToString().TrimStart('\"').TrimEnd('\"');
                string dname = jsonArray.GetObjectAt(i).GetNamedValue("dname").ToString().TrimStart('\"').TrimEnd('\"');
                DepartmentChanges.Add(new DepartmentChange(cid, ename, cwname, cdate, dname));
            }
        }
    }
}
