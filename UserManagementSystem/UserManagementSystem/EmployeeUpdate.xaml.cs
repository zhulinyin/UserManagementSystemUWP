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
    public sealed partial class EmployeeUpdate : Page
    {
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        public EmployeeUpdate()
        {
            this.InitializeComponent();
            if (employeeViewModel.SelectedItem != null)
            {
                Name.Text = employeeViewModel.SelectedItem.Ename;
                string birth = employeeViewModel.SelectedItem.Ebirth;
                Birthday.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(birth.Substring(0, 4)), Convert.ToInt32(birth.Substring(5, 2)),
                    Convert.ToInt32(birth.Substring(8, 2))));
                Location.Text = employeeViewModel.SelectedItem.Ehometown;
                Health.Text = employeeViewModel.SelectedItem.Ebody;
            }
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string date = Birthday.Date.Year + "-" + Birthday.Date.Month + "-" + Birthday.Date.Day;
            string location = Location.Text;
            string health = Health.Text;
            bool isSuccess = await employeeViewModel.UpdateItem(employeeViewModel.SelectedItem.Eid, name, date, location, health);
            if (isSuccess)
            {
                employeeViewModel.SelectedItem = null;
                Frame.Navigate(typeof(EmployeePage));
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
            if (employeeViewModel.SelectedItem != null)
            {
                Name.Text = employeeViewModel.SelectedItem.Ename;
                string birth = employeeViewModel.SelectedItem.Ebirth;
                Birthday.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(birth.Substring(0, 4)), Convert.ToInt32(birth.Substring(5, 2)),
                    Convert.ToInt32(birth.Substring(8, 2))));
                Location.Text = employeeViewModel.SelectedItem.Ehometown;
                Health.Text = employeeViewModel.SelectedItem.Ebody;
            }
        }
    }
}
