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
    /// Interaction logic for ShowAllPlaylistListPage.xaml
    /// </summary>
    /// 
   
    public partial class ShowAllPlaylistListPage : Page
    {
        MainWindow mainWindow;
        List<Playlist> playlists = new List<Playlist>();

        public ShowAllPlaylistListPage(MainWindow mainWindow, string type)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;

            List<Playlist> result = null;
            if (type == "all")
            {
                result = ApiRequests.GetAllPlaylists().Result;
                listNameTextBlock.Text = "Explore New Playlists";
            }
            else if (type == "own")
            {
                result = ApiRequests.GetOwnPlaylists().Result;
                listNameTextBlock.Text = "Added Playlists";
                
            }
            if (result != null)
            {

                try
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        var playlist = result[i];
                        var playlistPicture = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
                        var toAdd = new { playlistID = Convert.ToString(playlist.playlistID), playlistName = playlist.name, playlistPic = playlistPicture };
                        playlists.Add(playlist);
                        playlistItemsControl.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }

        }

        private void Playlist_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int playlistID = Convert.ToInt32(((StackPanel)sender).Tag);
            Playlist playlist = null;
            foreach (Playlist p in playlists)
            {
                if (p.playlistID == playlistID)
                {
                    playlist = p;
                    break;
                }
            }
            mainWindow.mainFrame.Navigate(new PlaylistPage(playlist, mainWindow));
        }

        

    
   
    }
}
