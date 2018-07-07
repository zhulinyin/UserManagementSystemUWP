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
    class UserViewModel
    {
        private static UserViewModel uniqueInstance;
        public static UserViewModel getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new UserViewModel();
            return uniqueInstance;
        }

        private ObservableCollection<User> users = new ObservableCollection<User>();
        public ObservableCollection<User> Users { get { return users; } }

        public User SelectedItem;

        public UserViewModel()
        {
            SelectedItem = null;
            ResolveJson();
        }

        static private async Task<string> GetUsersAsync()
        {
            string str = null;
            HttpResponseMessage response = await App.client.GetAsync("/GetUser");
            if (response.IsSuccessStatusCode)
            {
                str = await response.Content.ReadAsStringAsync();
            }
            return str;
        }

        private async void ResolveJson()
        {
            Users.Clear();
            string str = await GetUsersAsync();
            if (str == null) return;
            JsonArray jsonArray = JsonArray.Parse(str);
            for (uint i = 0; i < jsonArray.Count; i++)
            {
                string username = jsonArray.GetObjectAt(i).GetNamedValue("username").ToString().TrimStart('\"').TrimEnd('\"');
                string uright = jsonArray.GetObjectAt(i).GetNamedValue("uright").ToString().TrimStart('\"').TrimEnd('\"');
                if (uright == "1")
                {
                    uright = "管理员";
                }
                else uright = "普通";
                Users.Add(new User(username,uright));
            }

        }

        public async Task<bool> UpdateItem(string username, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"username",username },
                {"password",password }
            });
            var response = await App.client.PutAsync("/updateUser", content);
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
