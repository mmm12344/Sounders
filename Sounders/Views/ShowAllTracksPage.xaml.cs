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
    /// Interaction logic for ShowAllListPage.xaml
    /// </summary>
    public partial class ShowAllTracksPage : Page
    {
        MainWindow mainWindow;
        public ShowAllTracksPage(MainWindow mainWindow, string type)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;

            List<SongInfo> result = null;
            if (type == "all")
            {
                result = ApiRequests.GetAllSongs().Result;
                listNameTextBlock.Text = "Explore New Tracks";
            }
            else if (type == "liked")
            {
                result = ApiRequests.GetLikedSongs().Result;
                listNameTextBlock.Text = "Liked Tracks";
            }
            else if (type == "own")
            {
                result = ApiRequests.GetOwnSongs().Result;
                listNameTextBlock.Text = "Added Tracks";
            }
            if (result != null)
            {

                try
                {
                    for (int i = 0; i < result.Count; i++)
                    {
                        var song = result[i];
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songID = Convert.ToString(song.songID), songName = song.name, songPic = songPicture };
                        tracksItemsControl.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }

        private void Song_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void addToPlaylistItem_Click(object sender, RoutedEventArgs e)
        {

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

        private void Tracks_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
