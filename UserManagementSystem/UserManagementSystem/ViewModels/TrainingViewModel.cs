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
    class TrainingViewModel
    {
        private static TrainingViewModel uniqueInstance;
        public static TrainingViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new TrainingViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<Training> trainings = new ObservableCollection<Training>();
        public ObservableCollection<Training> Trainings { get { return trainings; } }

        public Training SelectedItem;

        public TrainingViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetTrainingsAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetTraining");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            Trainings.Clear();
            string str = await GetTrainingsAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string tid = jsonArray.GetObjectAt(i).GetNamedValue("tid").ToString().TrimStart('\"').TrimEnd('\"');
                string ename = jsonArray.GetObjectAt(i).GetNamedValue("ename").ToString().TrimStart('\"').TrimEnd('\"');
                string name = jsonArray.GetObjectAt(i).GetNamedValue("name").ToString().TrimStart('\"').TrimEnd('\"');
                string bdate = jsonArray.GetObjectAt(i).GetNamedValue("bdate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                string edate = jsonArray.GetObjectAt(i).GetNamedValue("edate").ToString().TrimStart('\"').TrimEnd('\"').Substring(0, 10);
                Trainings.Add(new Training(tid, ename, name, bdate, edate));
            }
        }

        public async Task<bool> CreateItem(string eid, string way, string bdate, string edate)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"way",way },
                {"bdate",bdate },
                {"edate",edate }
            });
            var response = await App.client.PostAsync("/newTraining", content);
            var resdata = await response.Content.ReadAsStringAsync();
            if (resdata.Equals("true"))
            {
                ResolveJson();
                return true;
            }
            else return false;
        }

        public async Task<bool> UpdateItem(string eid, string way, string bdate, string edate)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"eid",eid },
                {"way",way },
                {"bdate",bdate },
                {"edate",edate }
            });
            var response = await App.client.PutAsync("/updateTraning", content);
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
