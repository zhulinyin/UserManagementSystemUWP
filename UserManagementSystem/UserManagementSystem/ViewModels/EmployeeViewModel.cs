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
    class EmployeeViewModel
    {
        private static EmployeeViewModel uniqueInstance;
        public static EmployeeViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new EmployeeViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Employee> employees = new ObservableCollection<Employee>();
        public ObservableCollection<Employee> Employees { get { return employees; } }

        public Employee SelectedItem;

        public EmployeeViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetEmployeesAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetEmployee");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            string str = await GetEmployeesAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for(uint i = 0; i < jsonArray.Count; i++)
            {
                string eid = jsonArray.GetObjectAt(i).GetNamedValue("eid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string dname = jsonArray.GetObjectAt(i).GetNamedValue("dname").ToString().TrimStart('\"').TrimEnd('\"');
                string ebirth = jsonArray.GetObjectAt(i).GetNamedValue("ebirth").ToString().TrimStart('\"').TrimEnd('\"').Substring(0,10);
                string status = jsonArray.GetObjectAt(i).GetNamedValue("name").ToString().TrimStart('\"').TrimEnd('\"');
                string esex = jsonArray.GetObjectAt(i).GetNamedValue("esex").ToString().TrimStart('\"').TrimEnd('\"');
                string ehometown = jsonArray.GetObjectAt(i).GetNamedValue("ehometown").ToString().TrimStart('\"').TrimEnd('\"');
                string ebody = jsonArray.GetObjectAt(i).GetNamedValue("ebody").ToString().TrimStart('\"').TrimEnd('\"');
                Employees.Add(new Employee(eid, ename, dname, ebirth, status, esex, ehometown, ebody));
            }
            
        }
    }
}
