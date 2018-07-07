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
    class RewardViewModel
    {
        private static RewardViewModel uniqueInstance;
        public static RewardViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new RewardViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Reward> rewards = new ObservableCollection<Reward>();
        public ObservableCollection<Reward> Rewards { get { return rewards; } }

        public Reward SelectedItem;

        public RewardViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetRewardsAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetRewards");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            Rewards.Clear();
            string str = await GetRewardsAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string rid = jsonArray.GetObjectAt(i).GetNamedValue("rid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string rreason = jsonArray.GetObjectAt(i).GetNamedValue("rreason").ToString().TrimStart('\"').TrimEnd('\"');
                string name = jsonArray.GetObjectAt(i).GetNamedValue("name").ToString().TrimStart('\"').TrimEnd('\"');
                string rdate = jsonArray.GetObjectAt(i).GetNamedValue("rdate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                string rmoney = jsonArray.GetObjectAt(i).GetNamedValue("rmoney").ToString().TrimStart('\"').TrimEnd('\"');
                Rewards.Add(new Reward(rid, ename, rreason, name, rdate, rmoney));
            }
        }

        public async Task<bool> CreateItem(string eid, string rreason, string rway, string rdate, string rmoney)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"rreason",rreason },
                {"rway",rway },
                {"rdate",rdate },
                {"rmoney",rmoney }
            });
            var response = await App.client.PostAsync("/newReward", content);
            var resdata = await response.Content.ReadAsStringAsync();
            if (resdata.Equals("true"))
            {
                ResolveJson();
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateItem(string eid, string rreason, string rway, string rdate, string rmoney)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"rreason",rreason },
                {"rway",rway },
                {"rdate",rdate },
                {"rmoney",rmoney }
            });
            var response = await App.client.PutAsync("/updateReward", content);
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
