using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
    public sealed partial class EmployeeAdd : Page
    {
        private DepartmentViewModel ViewModel = DepartmentViewModel.getInstance();
        private EmployeeViewModel employeeViewModel = EmployeeViewModel.getInstance();
        public EmployeeAdd()
        {
            this.InitializeComponent();            
        }

        private void datePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {

        }

        private async void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {
            string name = Name.Text;
            string date = Birthday.Date.Year + "-" + Birthday.Date.Month + "-" + Birthday.Date.Day;
            string location = Location.Text;
            string health = Health.Text;
            string sex = ((ComboBoxItem)SexSelect.SelectedItem).Content.ToString();
            string department = ((Department)DeptSelect.SelectedItem).Dname;
            bool isSuccess = await employeeViewModel.CreateItem(name, date, sex, location, health, department);
            if (isSuccess)
            {
                Frame.Navigate(typeof(EmployeePage));
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
            Birthday.Date = DateTimeOffset.Now;
            Location.Text = "";
            Health.Text = "";

        }

        private void Sex_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Dept_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
