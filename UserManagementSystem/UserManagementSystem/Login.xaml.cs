using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UserManagementSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class Login : Page
    {
        public Login()
        {
            this.InitializeComponent();
            
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = Username_TextBox.Text;
            string password = Password_TextBox.Password;
            string str = await LoginAsync("/signin", username, password);
            if (str == "false")
            {
                showDialog("用户名或者密码错误");
                return;
            }
            JsonObject json = JsonObject.Parse(str);
            string uright = json.GetNamedValue("uright").ToString().TrimStart('\"').TrimEnd('\"');
            if (uright.Equals("1"))
            {
                App.manager = true;
                Frame.Navigate(typeof(NavigationFrame));
            }
            else if (uright.Equals("2"))
            {
                App.manager = false;
                Frame.Navigate(typeof(NavigationFrame));
            }
        }

        private async void showDialog(string text)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(text);
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("关闭"));
            await messageDialog.ShowAsync();
        }

        static async Task<string> LoginAsync(string path, string username, string password)
        {
            var content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                {"username",username },
                {"password",password }
            });
            var response = await App.client.PostAsync(path, content);
            var resdata = await response.Content.ReadAsStringAsync();
            return resdata;
        }
    }
}
