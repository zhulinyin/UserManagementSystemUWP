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
    class DepartmentViewModel
    {
        private static DepartmentViewModel uniqueInstance;
        public static DepartmentViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new DepartmentViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Department> departments = new ObservableCollection<Department>();
        public ObservableCollection<Department> Departments { get { return departments; } }

        public Department SelectedItem;

        public DepartmentViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetDepartmentsAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetDepartment");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            Departments.Clear();
            string str = await GetDepartmentsAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string did = jsonArray.GetObjectAt(i).GetNamedValue("did").ToString().TrimStart('\"').TrimEnd('\"');
                string dname = jsonArray.GetObjectAt(i).GetNamedValue("dname").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                Departments.Add(new Department(did, dname, ename));
            }
        }

        public async Task<bool> CreateItem(string name, string eid)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"name",name },
                {"eid",eid }
            });
            var response = await App.client.PostAsync("/newDepartment", content);
            var resdata = await response.Content.ReadAsStringAsync();
            if (resdata.Equals("true"))
            {
                ResolveJson();
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateItem(string did, string name, string eid)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"did",did },
                {"name",name },
                {"eid",eid }
            });
            var response = await App.client.PutAsync("/updateDepartment", content);
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
