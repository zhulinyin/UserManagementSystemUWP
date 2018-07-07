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
    public sealed partial class TrainingAdd : Page
    {
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        private TrainingViewModel trainingViewModel = TrainingViewModel.getInstance();

        public TrainingAdd()
        {
            this.InitializeComponent();
        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string beginTime = BeginTime.Date.Year + "-" + BeginTime.Date.Month + "-" + BeginTime.Date.Day;
            string endTime = EndTime.Date.Year + "-" + EndTime.Date.Month + "-" + EndTime.Date.Day;
            string way = ((ComboBoxItem)Way.SelectedItem).Content.ToString();
            if (way.Equals("student"))
            {
                way = "1";
            }
            else if (way.Equals("teacher"))
            {
                way = "2";
            }

            string eid = ((Employee)Employee.SelectedItem).Eid;
            bool isSuccess = await trainingViewModel.CreateItem(eid, way, beginTime, endTime);
            if (isSuccess)
            {
                Frame.Navigate(typeof(TrainingPage));
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
            BeginTime.Date = DateTimeOffset.Now;
            EndTime.Date = DateTimeOffset.Now;
        }
    }
}
