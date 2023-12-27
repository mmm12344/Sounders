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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
         //Test
        public HomePage()
        {
            InitializeComponent();


            if (HelperMethods.CheckIfSignedIn())
            {
                AddLikedSongs();
                AddOwnSongs();
                AddOwnPlaylist();
            }




        }


        private void AddLikedSongs()
        {
            List<SongInfo> result = ApiRequests.GetLikedSongs().Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songName = song.name, songPic = songPicture };
                        likedTracksList.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }

        private void AddOwnSongs()
        {
            List<SongInfo> result = ApiRequests.GetOwnSongs().Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songName = song.name, songPic = songPicture };
                        addedTracksList.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }

        private void AddOwnPlaylist()
        {
            List<Playlist> result = ApiRequests.GetAllPlaylists().Result;
            if (result != null)
            {

                try
                {
                    foreach (Playlist playlist in result)
                    {
                        var playlistPicture = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
                        var toAdd = new { playlistName = playlist.name, playlistPic = playlistPicture };
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
    }
}
