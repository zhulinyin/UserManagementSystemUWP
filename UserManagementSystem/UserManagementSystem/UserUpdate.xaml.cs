using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UserManagementSystem.ViewModels;
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
    public sealed partial class UserUpdate : Page
    {
        private UserViewModel ViewModel = UserViewModel.getInstance();

        public UserUpdate()
        {
            this.InitializeComponent();
            if (ViewModel.SelectedItem != null)
            {
                Name.Text = ViewModel.SelectedItem.Username;
                Password.Text = "";
            }
        }

        private async void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string username = Name.Text;
            string password = Password.Text;
            
            bool isSuccess = await ViewModel.UpdateItem(username, password);
            if (isSuccess)
            {
                Frame.Navigate(typeof(UserPage));
            }
            else
            {
                showDialog("更新失败");
            }
        }

        private async void showDialog(string text)
        {
            var messageDialog = new Windows.UI.Popups.MessageDialog(text);
            messageDialog.Commands.Add(new Windows.UI.Popups.UICommand("关闭"));
            await messageDialog.ShowAsync();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.SelectedItem != null)
            {
                Name.Text = ViewModel.SelectedItem.Username;
                Password.Text = "";
            }
        }
    }
}
