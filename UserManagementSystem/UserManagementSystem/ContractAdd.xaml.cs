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
    public sealed partial class ContractAdd : Page
    {
        private ContractViewModel contractViewModel = ContractViewModel.getInstance();
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        public ContractAdd()
        {
            this.InitializeComponent();
        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string salary = Salary.Text;
            string beginTime = BeginTime.Date.Year + "-" + BeginTime.Date.Month + "-" + BeginTime.Date.Day;
            string endTime = EndTime.Date.Year + "-" + EndTime.Date.Month + "-" + EndTime.Date.Day;
            string eid = ((Employee)Employee.SelectedItem).Eid;
            bool isSuccess = await contractViewModel.CreateItem(eid, salary, beginTime, endTime);
            if (isSuccess)
            {
                Frame.Navigate(typeof(ContractPage));
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
            Salary.Text = "";
            BeginTime.Date = DateTimeOffset.Now;
            EndTime.Date = DateTimeOffset.Now;
        }
    }
}
