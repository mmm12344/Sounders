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
    /// Interaction logic for PlaylistPage.xaml
    /// </summary>
    public partial class PlaylistPage : Page
    {
        private List<SongData> playlistSongs = new List<SongData>();
        MainWindow mainWindow;
        PlayerQueue mainQueue;
        int playlistID;
        Playlist playlist;
        public PlaylistPage(Playlist playlist, MainWindow mainWindow)
        {
            InitializeComponent();

            HelperMethods.OpenSignInIfNotSigned();
            this.mainWindow = mainWindow;
            this.mainQueue = mainWindow.mainQueue;
            playlistID = playlist.playlistID;
            this.playlist = playlist;

            playlistImage.Source = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
            this.playlistName.Text = playlist.name;

            List<SongInfo> result = ApiRequests.GetPlaylistSongs(playlist.playlistID).Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songID = Convert.ToString(song.songID), songName = song.name, songPic = songPicture };
                        playlistSongs.Add(new SongData(song.songID, song.name, songPicture));
                        playlistSongsList.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }

        }

    

        private void removeFromPLaylist_Click(object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            var result = ApiRequests.RemoveSongFromPlaylist(playlistID, songID).Result;
            if (result)
            {
                HelperMethods.SuccessMessage("Removed Song From Playlist");
                mainWindow.mainFrame.Navigate(new PlaylistPage(playlist, mainWindow));
            }
            else
            {
                HelperMethods.ErrorMessage("Error, Please Try Again.");
            }
        }

        private void ListenButton_Click(object sender, RoutedEventArgs e)
        {
            mainQueue.EnqueueFromList(playlistSongs);
            mainQueue.AddCurrentSongToPlayer();
        }

       

        private void addToQueueItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
