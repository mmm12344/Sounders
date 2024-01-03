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
        public SearchPage(string searchText)
        {
            InitializeComponent();

            HelperMethods.OpenSignInIfNotSigned();

            List<SongInfo> result = ApiRequests.Search(searchText).Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songID = Convert.ToString(song.songID), songName = song.name, songPic = songPicture };
                        searchResultsList.Items.Add(toAdd);
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

        }

        private void addToQueueItem_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
