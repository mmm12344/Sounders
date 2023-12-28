using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sounders.Views
{
    /// <summary>
    /// Interaction logic for AddTrackorPlaylist.xaml
    /// </summary>
    public partial class AddTrackorPlaylist : Page
    {
        MainWindow mainWindow;
        public AddTrackorPlaylist(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        private void createNewPlaylistToPage_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new AddPlayListPage(mainWindow));

        }

        

        private void addTracksToPage_Click(object sender, RoutedEventArgs e)
        {
            
            this.NavigationService.Navigate(new AddTrackPage(mainWindow));
           
        }
       

    }
}
