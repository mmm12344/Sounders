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

        List<Track> my = new List<Track>();
        private bool isMoving = false;                  //False - ignore mouse movements and don't scroll
        private bool isDeferredMovingStarted = false;   //True - Mouse down -> Mouse up without moving -> Move; False - Mouse down -> Move
        private Point? startPosition = null;
        private double slowdown = 200;                  //The number 200 is found from experiments, it should be corrected
       


         //Test
        public HomePage()
        {
            InitializeComponent();



           
            AddLikedSongs();
            AddAllSongs();
            AddOwnSongs();
            AddOwnPlaylist();


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

        private void AddAllSongs()
        {
            List<SongInfo> result = ApiRequests.GetAllSongs().Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songName = song.name, songPic = songPicture };
                        exploreNewTracks.Items.Add(toAdd);
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }
        }


        private void ScrollViewer_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.isMoving == true) //Moving with a released wheel and pressing a button
                this.CancelScrolling();
            else if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                if (this.isMoving == false) //Pressing a wheel the first time
                {
                    this.isMoving = true;
                    this.startPosition = e.GetPosition(sender as IInputElement);
                    this.isDeferredMovingStarted = true; //the default value is true until the opposite value is set

                    this.AddScrollSign(e.GetPosition(this.topLayer).X, e.GetPosition(this.topLayer).Y);
                }
            }
        }

        private void ScrollViewer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Released && this.isDeferredMovingStarted != true)
                this.CancelScrolling();
        }

        private void CancelScrolling()
        {
            this.isMoving = false;
            this.startPosition = null;
            this.isDeferredMovingStarted = false;
            this.RemoveScrollSign();
        }

        private void ScrollViewer_MouseMove(object sender, MouseEventArgs e)
        {
            var sv = sender as ScrollViewer;

            if (this.isMoving && sv != null)
            {
                this.isDeferredMovingStarted = false; //standard scrolling (Mouse down -> Move)

                var currentPosition = e.GetPosition(sv);
                var offset = currentPosition - startPosition.Value;
                offset.Y /= slowdown;
                offset.X /= slowdown;

                //if(Math.Abs(offset.Y) > 25.0/slowdown)  //Some kind of a dead space, uncomment if it is neccessary
                sv.ScrollToVerticalOffset(sv.VerticalOffset + offset.Y);
                sv.ScrollToHorizontalOffset(sv.HorizontalOffset + offset.X);
            }
        }
        private void AddScrollSign(double x, double y)
        {
            int size = 50;
            var img = new BitmapImage(new Uri(@"Static/Images/ScrollIcon.png",UriKind.Relative));
            var adorner = new Image() { Source = img, Width = size, Height = size };
            //var adorner = new Ellipse { Stroke = Brushes.Red, StrokeThickness = 2.0, Width = 20, Height = 20 };

            this.topLayer.Children.Add(adorner);
            Canvas.SetLeft(adorner, x - size / 2);
            Canvas.SetTop(adorner, y - size / 2);
        }

        private void RemoveScrollSign()
        {
            this.topLayer.Children.Clear();
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
    }
}
