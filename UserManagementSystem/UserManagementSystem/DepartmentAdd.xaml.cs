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
    public sealed partial class DepartmentAdd : Page
    {
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        private DepartmentViewModel departmentViewModel = DepartmentViewModel.getInstance();
        public DepartmentAdd()
        {
            this.InitializeComponent();
        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            Employee em = ((Employee)Employee.SelectedItem);
            string eid;
            if (em == null)
            {
                eid = "0";
            }
            else eid = em.Eid;
            bool isSuccess = await departmentViewModel.CreateItem(name, eid);
            if (isSuccess)
            {
                Frame.Navigate(typeof(DepartmentPage));
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
            Name.Text = "";
        }
    }
}
