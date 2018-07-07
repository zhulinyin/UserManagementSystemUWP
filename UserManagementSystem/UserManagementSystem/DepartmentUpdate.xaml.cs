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
    public sealed partial class DepartmentUpdate : Page
    {
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        private DepartmentViewModel departmentViewModel = DepartmentViewModel.getInstance();

        public DepartmentUpdate()
        {
            this.InitializeComponent();
            if (departmentViewModel.SelectedItem != null)
            {
                Name.Text = departmentViewModel.SelectedItem.Dname;
                Employee item = null;
                for (int i = 0; i < employeeViewModel.Employees.Count; i++)
                {
                    if (employeeViewModel.Employees[i].Ename == departmentViewModel.SelectedItem.Ename)
                    {
                        item = employeeViewModel.Employees[i];
                    }
                }
                Employee.SelectedItem = item;
            }
        }

        private async void UpdateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string eid = ((Employee)Employee.SelectedItem).Eid;
            bool isSuccess = await departmentViewModel.UpdateItem(departmentViewModel.SelectedItem.Did,name,eid);
            if (isSuccess)
            {
                departmentViewModel.SelectedItem = null;
                Frame.Navigate(typeof(DepartmentPage));
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
            if (departmentViewModel.SelectedItem != null)
            {
                Name.Text = departmentViewModel.SelectedItem.Dname;
                Employee item = null;
                for (int i = 0; i < employeeViewModel.Employees.Count; i++)
                {
                    if (employeeViewModel.Employees[i].Ename == departmentViewModel.SelectedItem.Ename)
                    {
                        item = employeeViewModel.Employees[i];
                    }
                }
                Employee.SelectedItem = item;
            }
        }
    }
}
