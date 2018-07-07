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
    class ContractViewModel
    {
        private static ContractViewModel uniqueInstance;
        public static ContractViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new ContractViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Contract> contracts = new ObservableCollection<Contract>();
        public ObservableCollection<Contract> Contracts { get { return contracts; } }

        public Contract SelectedItem;

        public ContractViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetContractsAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetContracts");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            Contracts.Clear();
            string str = await GetContractsAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string cid = jsonArray.GetObjectAt(i).GetNamedValue("cid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string salary = jsonArray.GetObjectAt(i).GetNamedValue("salary").ToString().TrimStart('\"').TrimEnd('\"');
                string bdate = jsonArray.GetObjectAt(i).GetNamedValue("bdate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                string edate = jsonArray.GetObjectAt(i).GetNamedValue("edate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                Contracts.Add(new Contract(cid, ename, salary, bdate, edate));
            }
        }

        public async Task<bool> CreateItem(string eid, string salary, string bdate, string edate)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"salary",salary },
                {"bdate",bdate },
                {"edate",edate }
            });
            var response = await App.client.PostAsync("/newContract", content);
            var resdata = await response.Content.ReadAsStringAsync();
            if (resdata.Equals("true"))
            {
                ResolveJson();
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateItem(string eid, string salary, string bdate, string edate)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"salary",salary },
                {"bdate",bdate },
                {"edate",edate }
            });
            var response = await App.client.PutAsync("/updateContract", content);
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
