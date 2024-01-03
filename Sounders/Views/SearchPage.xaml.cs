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
    /// Interaction logic for SearchPage.xaml
    /// </summary>
    public partial class SearchPage : Page
    {
        List<SongData> songs = new List<SongData>();
        MainWindow mainWindow;
        PlayerQueue mainQueue;
        public SearchPage(string searchText, MainWindow mainWindow)
        {
            InitializeComponent();

            this.mainWindow = mainWindow;
            this.mainQueue = mainWindow.mainQueue;

            HelperMethods.OpenSignInIfNotSigned();

            List<SongInfo> result = ApiRequests.Search(searchText).Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new SongData( song.songID, song.name,  songPicture );
                        searchResultsList.Items.Add(toAdd);
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

        private void Track_MouseDown(object sender, MouseButtonEventArgs e)
        {
            int songID = Convert.ToInt32(((StackPanel)sender).Tag);

            mainQueue.ClearAll();
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(songs, songID));
            mainQueue.AddCurrentSongToPlayer();
        }
    }
}
