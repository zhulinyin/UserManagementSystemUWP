﻿using System;
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

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace UserManagementSystem
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class EmployeeUpdate : Page
    {
        public EmployeeUpdate()
        {
            this.InitializeComponent();
        }


        private void datePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {

        }

        private void CreateButton_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Number.Text = "";
            Name.Text = "";
            Birthday.Date = DateTimeOffset.Now;
            Situation.Text = "";
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