using MusicPlayerGUI;
using MusicPlayerGUI.settings;
using Sounders.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
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
using System.Windows.Threading;
using static System.Net.Mime.MediaTypeNames;
using Image = System.Windows.Controls.Image;



namespace Sounders
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MediaPlayer mediaPlayer = new MediaPlayer();

        private DispatcherTimer timer;
        public bool isPlaying = false;
        public PlayerQueue mainQueue;



        public MainWindow()
        {

           
            InitializeComponent();

            mediaPlayer.MediaOpened += MediaPlayer_MediaOpened;
            mediaPlayer.MediaEnded += MediaPlayer_MediaEnded;

            this.mainQueue = new PlayerQueue(this);

            

            if(!ApiRequests.IsLive().Result)
            {
                mainFrame.Navigate(new AccountSettingsPage(this));
                return;
            }


            HelperMethods.OpenSignInIfNotSigned();

            

            if (Settings.GetServerUrl() == null)
            {
                mainFrame.Navigate(new AccountSettingsPage(this));
                return;
            }

            if (Settings.GetServerUrl() != null && Settings.GetUserID() != null && Settings.GetPassword() != null )
                ApiRequests.UpdateClient();

            mainFrame.Navigate(new HomePage(this));



            var currentSongUri = new Uri("Test/Test.mp3", UriKind.Relative);

            mediaPlayer.Open(currentSongUri);
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(0.5); // Update frequency
            timer.Tick += Timer_Tick;

        }



        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
            if (!isPlaying)
            {


                mediaPlayer.Play();
                timer.Start();

                Uri newImageUri = new Uri("Static/Images/Pause.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(newImageUri);
            
                Image image = new Image();
                image.Source = bitmapImage;
                image.Height = 20;
                image.Width = 20;
                playButton.Content = image;
                isPlaying = true;
            }

            else
            {
                mediaPlayer.Pause();
                timer.Stop();

                Uri newImageUri = new Uri("Static/Images/Play.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(newImageUri);

                Image image = new Image();
                image.Source = bitmapImage;
                image.Height = 20;
                image.Width = 20;
                playButton.Content = image;

               
                isPlaying = false;
            }



        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                TimeSpan newPosition = TimeSpan.FromSeconds(audioBar.Value);
                mediaPlayer.Position = newPosition;
            }

        }

        private void MediaPlayer_MediaOpened(object sender, EventArgs e)
        {
            if (mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                audioBar.Maximum = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
            }
        }

        private void MediaPlayer_MediaEnded(object sender, EventArgs e)
        {
            mediaPlayer.Close();
            mainQueue.Dequeue();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            
            audioBar.Value = mediaPlayer.Position.TotalSeconds;
           
        }

        private void ForwardButton_Click(object sender, RoutedEventArgs e)
        {
            mainQueue.Dequeue();
        }



        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Navigate(new HomePage(this));
          
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {

            mainFrame.Navigate(new AccountSettingsPage(this));
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            Settings.UpdateUserID(null);
            Settings.UpdatePassword(null);
            HelperMethods.OpenSignInWindow();
            
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            
            if (SearchBox.Text != string.Empty )
            {
                mainFrame.Navigate(new SearchPage(SearchBox.Text, this));
            }
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
          

            mainFrame.Navigate(new AddTrackorPlaylist(this));
            
             
        }

        private void LikeButton_Click(Object sender, RoutedEventArgs e)
        {
            int songID = Convert.ToInt32(likeButton.Tag);
            var isliked = ApiRequests.IsLiked(songID).Result;
            if (isliked)
            {
                var result = ApiRequests.RemoveLike(songID).Result;
                if (!result)
                {
                    HelperMethods.ErrorMessage("Could not remove Like, Please Try Again.");
                }
                else
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("Static/Images/WhiteHeart.png", UriKind.Relative));
                    image.Height = 20;
                    image.Width = 20;
                    likeButton.Content = image;
                }
            }
            else
            {
                var result = ApiRequests.AddLikeToSong(songID).Result;
                if (!result)
                {
                    HelperMethods.ErrorMessage("Could not add Like, Please Try Again.");
                }
                else
                {
                    Image image = new Image();
                    image.Source = new BitmapImage(new Uri("Static/Images/RedHeart.png", UriKind.Relative));
                    image.Height = 20;
                    image.Width = 20;
                    likeButton.Content = image;
                }
            }
        }

        private void ShuffleButton_Click(object sender, RoutedEventArgs e)
        {
            mainQueue.Randomize();
        }

        
    }
}
