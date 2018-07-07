using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class RewardPage : Page
    {
        private RewardViewModel ViewModel = RewardViewModel.getInstance();
        public RewardPage()
        {
            this.InitializeComponent();
        }

        private void AddAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RewardAdd));
        }

        private void Update(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(RewardUpdate));
        }

        private void Delete(object sender, RoutedEventArgs e)
        {

        }

        private void Setting_Clicked(object sender, RoutedEventArgs e)
        {
            var data = (sender as FrameworkElement).DataContext;
            var item = listview.ContainerFromItem(data) as ListViewItem;
            ViewModel.SelectedItem = item.Content as Models.Reward;
        }
    }
}
