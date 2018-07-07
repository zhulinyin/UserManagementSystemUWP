using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UserManagementSystem.Models;
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
    public sealed partial class RewardAdd : Page
    {
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        private RewardViewModel rewardViewModel = RewardViewModel.getInstance();
        public RewardAdd()
        {
            this.InitializeComponent();
        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string time = Time.Date.Year + "-" + Time.Date.Month + "-" + Time.Date.Day;
            string reason = Reason.Text;
            string way = ((ComboBoxItem)Way.SelectedItem).Content.ToString();
            string money = Money.Text;
            if (way.Equals("bonus"))
            {
                way = "1";
            }
            else if (way.Equals("fine"))
            {
                way = "2";
            }
            
            string eid = ((Employee)Employee.SelectedItem).Eid;
            bool isSuccess = await rewardViewModel.CreateItem(eid, reason, way, time, money);
            if (isSuccess)
            {
                Frame.Navigate(typeof(RewardPage));
            }
            else
            {
                showDialog("添加失败");
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
            Time.Date = DateTimeOffset.Now;
            Money.Text = "";
            Reason.Text = "";
        }
    }
}
