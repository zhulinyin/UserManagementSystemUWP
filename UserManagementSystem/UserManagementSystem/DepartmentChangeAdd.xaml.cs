using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UserManagementSystem.Models;
using UserManagementSystem.ViewModels;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UserManagementSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class DepartmentChangeAdd : Page
    {
        private DepartmentViewModel ViewModel = DepartmentViewModel.getInstance();
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        private DepartmentChangeViewModel departmentChangeViewModel = DepartmentChangeViewModel.getInstance();
        public DepartmentChangeAdd()
        {
            this.InitializeComponent();
        }

        private void datePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {

        }

        private void Dept_SelectionChanged2(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dept_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            Employee em = ((Employee)Employee.SelectedItem);
            string eid;
            if (em == null)
            {
                eid = "0";
            }
            else eid = em.Eid;
            string way = ((ComboBoxItem)Way.SelectedItem).Content.ToString();
            if (way.Equals("take office"))
            {
                way = "1";
            }
            else if (way.Equals("departure"))
            {
                way = "2";
            }
            else if(way== "transfer department")
            {
                way = "3";
            }
            string time = Time.Date.Year + "-" + Time.Date.Month + "-" + Time.Date.Day;
            string tdid=((Department)tDept.SelectedItem).Did;
            string fdname = em.Dname;
            string fdid = null;
            for(int i=0;i< ViewModel.Departments.Count; i++)
            {
                if(fdname== ViewModel.Departments[i].Dname)
                {
                    fdid = ViewModel.Departments[i].Did;
                }
            }
            bool isSuccess = await departmentChangeViewModel.CreateItem(eid, way,time,tdid,fdid);
            if (isSuccess)
            {
                Frame.Navigate(typeof(DepartmentChangePage));
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

        }
    }
}
