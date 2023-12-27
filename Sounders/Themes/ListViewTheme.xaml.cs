using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Navigation;
using System.Windows.Controls;
using Sounders.Views;
namespace Sounders.Themes
{
    public partial class ListViewTheme : ResourceDictionary
    {
        private void addToPlaylistItem_Click(object sender, RoutedEventArgs e)
        {
            NavigationService nav = NavigationService.GetNavigationService((Grid)((Image)sender).Parent);
            nav.Navigate(new Uri(@"Views/HomePage.xaml", UriKind.Relative));

        }

        private void likeItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void playItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void addToQueueItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
