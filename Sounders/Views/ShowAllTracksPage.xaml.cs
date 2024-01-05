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
        PlayerQueue mainQueue;
        List<SongData> songs = new List<SongData>();
        public ShowAllTracksPage(MainWindow mainWindow, string type)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this.mainQueue = mainWindow.mainQueue;

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
                        var toAdd = new SongData(song.songID, song.name, songPicture);
                        tracksItemsControl.Items.Add(toAdd);
                        songs.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }

        

        private void addToPlaylistItem_Click(object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            mainWindow.mainFrame.Navigate(new AddToPlayListPage(mainWindow, songID));
        }

        

        private void addToQueueItem_Click(object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(songs, songID));
        }

        private void Tracks_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int songID = Convert.ToInt32(((StackPanel)sender).Tag);

            mainQueue.ClearAll();
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(songs, songID));
            mainQueue.AddCurrentSongToPlayer();
        }
        private void removeItem_Click(object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            var result = ApiRequests.DeleteSong(songID).Result;
            if (result)
            {
                HelperMethods.SuccessMessage("Deleted Song Successfully!");
                mainWindow.mainFrame.Navigate(new HomePage(mainWindow));
            }
            else
            {
                HelperMethods.ErrorMessage("Error, Please Try Again.");
            }
        }
    }
}
