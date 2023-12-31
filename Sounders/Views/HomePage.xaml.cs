﻿using MusicPlayerGUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
        private double slowdown = 200;                
       
        private List<SongData> likedSongs = new List<SongData>();
        private List<SongData> ownSongs = new List<SongData>();
        private List<SongData> allSongs = new List<SongData>();
        private List<Playlist> yourPlaylists = new List<Playlist>();
        private List<Playlist> allPlaylists = new List<Playlist>();

        private MainWindow mainWindow;
        private PlayerQueue mainQueue;
        private MediaPlayer mediaPlayer;
         //Test
        public HomePage(MainWindow mainWindow)
        {
            InitializeComponent();

            HelperMethods.OpenSignInIfNotSigned();


            AddLikedSongs();
            AddAllSongs();
            AddOwnSongs();
            AddOwnPlaylist();
            AddAllPlaylist();

            this.mainWindow = mainWindow;
            this.mainQueue = mainWindow.mainQueue;
            this.mediaPlayer = mainWindow.mediaPlayer;

        }


        private void AddLikedSongs()
        {
            List<SongInfo> result = ApiRequests.GetLikedSongs().Result;
            if (result != null)
            {

                try
                {
                    for (int i = 0; i < result.Count && i < 3; i++)
                    {
                        SongInfo song = result[i];
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new {songID = Convert.ToString(song.songID) ,songName = song.name, songPic = songPicture };
                        likedSongs.Add(new SongData(song.songID, song.name, songPicture));
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
                    for (int i = 0; i < result.Count && i < 3; i++)
                    {
                        SongInfo song = result[i];
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new {songID = Convert.ToString(song.songID) ,songName = song.name, songPic = songPicture };
                        ownSongs.Add(new SongData(song.songID, song.name, songPicture));
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
            List<Playlist> result = ApiRequests.GetOwnPlaylists().Result;
            if (result != null)
            {

                try
                {
                    for (int i = 0; i < result.Count && i < 3; i++)
                    {
                        var playlist = result[i];
                        var playlistPicture = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
                        var toAdd = new {playlistID = Convert.ToString(playlist.playlistID),playlistName = playlist.name, playlistPic = playlistPicture };
                        yourPlaylists.Add(playlist);
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

        private void AddAllPlaylist()
        {
            List<Playlist> result = ApiRequests.GetAllPlaylists().Result;
            if (result != null)
            {

                try
                {
                    for (int i = 0; i < result.Count && i < 3; i++)
                    {
                        var playlist = result[i];
                        var playlistPicture = HelperMethods.GetBitmapImgFromBytes(playlist.picture);
                        var toAdd = new { playlistID = Convert.ToString(playlist.playlistID), playlistName = playlist.name, playlistPic = playlistPicture };
                        allPlaylists.Add(playlist);
                        explorenewPlaylists.Items.Add(toAdd);
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
                    for (int i = 0; i < result.Count && i < 3; i++)
                    {
                        var song = result[i];
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new {songID = Convert.ToString(song.songID) ,songName = song.name, songPic = songPicture };
                        allSongs.Add(new SongData(song.songID, song.name, songPicture));
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
            if (this.isMoving == true) 
                this.CancelScrolling();
            else if (e.ChangedButton == MouseButton.Middle && e.ButtonState == MouseButtonState.Pressed)
            {
                if (this.isMoving == false) 
                {
                    this.isMoving = true;
                    this.startPosition = e.GetPosition(sender as IInputElement);
                    this.isDeferredMovingStarted = true; 

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
                this.isDeferredMovingStarted = false; 

                var currentPosition = e.GetPosition(sv);
                var offset = currentPosition - startPosition.Value;
                offset.Y /= slowdown;
                offset.X /= slowdown;

    
                sv.ScrollToVerticalOffset(sv.VerticalOffset + offset.Y);
                sv.ScrollToHorizontalOffset(sv.HorizontalOffset + offset.X);
            }
        }
        private void AddScrollSign(double x, double y)
        {
            int size = 50;
            var img = new BitmapImage(new Uri(@"Static/Images/ScrollIcon.png",UriKind.Relative));
            var adorner = new Image() { Source = img, Width = size, Height = size };
   

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
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            mainWindow.mainFrame.Navigate(new AddToPlayListPage(mainWindow,songID));
        }



 

        private void addToQueueItem_Click(object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(((MenuItem)sender).Tag);
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(allSongs, songID));
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

        private void LikedTracks_MouseDown(object sender, MouseEventArgs e)
        {
            int songID = Convert.ToInt32(((StackPanel)sender).Tag);

            mainQueue.ClearAll();
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(likedSongs, songID));
            mainQueue.AddCurrentSongToPlayer();
        }

        private void AddedTracks_MouseDown(object sender, MouseEventArgs e)
        {
            int songID = Convert.ToInt32(((StackPanel)sender).Tag);

            mainQueue.ClearAll();
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(ownSongs, songID)); ;
            mainQueue.AddCurrentSongToPlayer();
        }

        private void ExploreNewTracks_MouseDown(object sender, MouseEventArgs e)
        {
            mediaPlayer.Close();
            int songID = Convert.ToInt32(((StackPanel)sender).Tag);

            mainQueue.ClearAll();
            mainQueue.Enqueue(HelperMethods.GetSongDataFromID(allSongs, songID)); ;
            mainQueue.AddCurrentSongToPlayer();
        }

        private void YourPlaylist_MouseDown(object sender, MouseEventArgs e)
        {
            int playlistID = Convert.ToInt32(((StackPanel)sender).Tag);
            Playlist playlist = null;
            foreach(Playlist p in yourPlaylists)
            {
                if(p.playlistID == playlistID)
                {
                    playlist = p;
                    break;
                }
            }
            mainWindow.mainFrame.Navigate(new PlaylistPage(playlist, mainWindow));
        }

        private void ExploreNewPlaylists_MouseDown(object sender, MouseEventArgs e)
        {
            int playlistID = Convert.ToInt32(((StackPanel)sender).Tag);
            Playlist playlist = null;
            foreach (Playlist p in allPlaylists)
            {
                if (p.playlistID == playlistID)
                {
                    playlist = p;
                    break;
                }
            }
            mainWindow.mainFrame.Navigate(new PlaylistPage(playlist, mainWindow));
        }

        private void ShowAllTracks_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new ShowAllTracksPage(mainWindow, "all"));
        }

        private void ShowAllPlaylists_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new ShowAllPlaylistListPage(mainWindow, "all"));
        }

        private void ShowAllLikedTrack_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new ShowAllTracksPage(mainWindow, "liked"));
        }

        private void ShowAllAddedTrack_Click(Object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new ShowAllTracksPage(mainWindow, "own"));
        }

        private void ShowAllAddedPlaylist_Click(Object sender, RoutedEventArgs e)
        {
            mainWindow.mainFrame.Navigate(new ShowAllPlaylistListPage(mainWindow, "own"));
        }

    

        private void removePlaylist_Click(object sender, RoutedEventArgs e)
        {
            int playlistID = Convert.ToInt32(((MenuItem)sender).Tag);
            var result = ApiRequests.DeletePlaylist(playlistID).Result;
            if (result)
            {
                HelperMethods.SuccessMessage("Deleted Playlist Successfully!");
                mainWindow.mainFrame.Navigate(new HomePage(mainWindow));
            }
            else
            {
                HelperMethods.ErrorMessage("Error, Please Try Again.");
            }
        }
    }
}
