using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UserManagementSystem;
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
    public sealed partial class NavigationFrame : Page
    {
        public NavigationFrame()
        {
            this.InitializeComponent();
            contentFrame.Navigate(typeof(MainPage));
        }

        private void nvAll_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            //先判断是否选中了setting
            if (args.IsSettingsInvoked)
            {
                //contentFrame.Navigate(typeof(Sync));
            }
            else
            {
                //选中项的内容
                switch (args.InvokedItem)
                {
                    case "首页":
                        contentFrame.Navigate(typeof(MainPage));
                        break;
                    case "员工":
                        contentFrame.Navigate(typeof(EmployeePage));
                        break;
                    case "部门":
                        contentFrame.Navigate(typeof(DepartmentPage));
                        break;
                    case "合同":
                        contentFrame.Navigate(typeof(ContractPage));
                        break;
                    case "考勤":
                        contentFrame.Navigate(typeof(AttendancePage));
                        break;
                    case "部门变更":
                        contentFrame.Navigate(typeof(DepartmentChangePage));
                        break;
                    case "用户":
                        contentFrame.Navigate(typeof(UserPage));
                        break;
                    case "奖惩":
                        contentFrame.Navigate(typeof(RewardPage));
                        break;
                    case "培训":
                        contentFrame.Navigate(typeof(TrainingPage));
                        break;
                    default:
                        contentFrame.Navigate(typeof(MainPage));
                        break;
                }
            }
        }

        private void BackToMain(object sender, RoutedEventArgs e)
        {
            contentFrame.Navigate(typeof(MainPage));
        }
    }
}
