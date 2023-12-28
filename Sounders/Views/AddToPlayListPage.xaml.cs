using MusicPlayerGUI;
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
    /// Interaction logic for AddToPlayListPage.xaml
    /// </summary>
    public partial class AddToPlayListPage : Page
    {
        int songID;
        MainWindow mainWindow;
        public AddToPlayListPage(MainWindow mainWindow, int songID)
        {
            InitializeComponent();
            this.songID = songID;
            this.mainWindow = mainWindow;

            List<Playlist> result = ApiRequests.GetOwnPlaylists().Result;
            if (result != null)
            {

                try
                {
                    foreach (Playlist playlist in result)
                    {
                        var playlistPicture = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
                        var toAdd = new { playlistID = Convert.ToString(playlist.playlistID), playlistName = playlist.name, playlistPic = playlistPicture };
                        YourPlaylists.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }

        public void AddToPlaylist_Click(Object sender, RoutedEventArgs e)
        {
            int playlistID = Convert.ToInt32(((Button)sender).Tag);
            var result = ApiRequests.AddSongToPlaylist(playlistID, songID).Result;
            if (result)
            {
                HelperMethods.SuccessMessage("Added Song to Playlist!");
                mainWindow.mainFrame.Navigate(new HomePage(mainWindow));
            }
            else
            {
                HelperMethods.ErrorMessage("Could not Add Song to Playlist, Please Try Again.");
            }
        }

    }
}
