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
    public sealed partial class ContractUpdate : Page
    {
        private ContractViewModel contractViewModel = ContractViewModel.getInstance();
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();

        public ContractUpdate()
        {
            this.InitializeComponent();
            if (contractViewModel.SelectedItem != null)
            {
                string bdate = contractViewModel.SelectedItem.Bdate;
                BeginTime.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(bdate.Substring(0, 4)), Convert.ToInt32(bdate.Substring(5, 2)),
                    Convert.ToInt32(bdate.Substring(8, 2))));
                string edate= contractViewModel.SelectedItem.Edate;
                EndTime.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(edate.Substring(0, 4)), Convert.ToInt32(edate.Substring(5, 2)),
                    Convert.ToInt32(edate.Substring(8, 2))));
                Salary.Text = contractViewModel.SelectedItem.Salary;
                Employee item = null;
                for(int i=0;i< employeeViewModel.Employees.Count; i++)
                {
                    if(employeeViewModel.Employees[i].Ename == contractViewModel.SelectedItem.Ename)
                    {
                        item = employeeViewModel.Employees[i];
                    }
                }
                Employee.SelectedItem = item;
            }
        }

        private async void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string salary = Salary.Text;
            string beginTime = BeginTime.Date.Year + "-" + BeginTime.Date.Month + "-" + BeginTime.Date.Day;
            string endTime = EndTime.Date.Year + "-" + EndTime.Date.Month + "-" + EndTime.Date.Day;
            string eid = ((Employee)Employee.SelectedItem).Eid;
            bool isSuccess = await contractViewModel.UpdateItem(eid, salary, beginTime, endTime);
            if (isSuccess)
            {
                contractViewModel.SelectedItem = null;
                Frame.Navigate(typeof(ContractPage));
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
            if (contractViewModel.SelectedItem != null)
            {
                string bdate = contractViewModel.SelectedItem.Bdate;
                BeginTime.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(bdate.Substring(0, 4)), Convert.ToInt32(bdate.Substring(5, 2)),
                    Convert.ToInt32(bdate.Substring(8, 2))));
                string edate = contractViewModel.SelectedItem.Edate;
                EndTime.Date = new DateTimeOffset(new DateTime(Convert.ToInt32(edate.Substring(0, 4)), Convert.ToInt32(edate.Substring(5, 2)),
                    Convert.ToInt32(edate.Substring(8, 2))));
                Salary.Text = contractViewModel.SelectedItem.Salary;
                Employee item = null;
                for (int i = 0; i < employeeViewModel.Employees.Count; i++)
                {
                    if (employeeViewModel.Employees[i].Ename == contractViewModel.SelectedItem.Ename)
                    {
                        item = employeeViewModel.Employees[i];
                    }
                }
                Employee.SelectedItem = item;
            }
        }
    }
}
