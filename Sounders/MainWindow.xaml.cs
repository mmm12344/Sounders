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
        public MediaPlayer playMedia = new MediaPlayer();
        private DispatcherTimer timer;
        private string trackState="stop";

        

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            HelperMethods.CloseAllWindows();
        }

        public MainWindow()
        {

           
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;


            HelperMethods.OpenSignInIfNotSigned();

            

            if (Settings.GetServerUrl() == null)
            {
                mainFrame.Navigate(new AccountSettingsPage());
                return;
            }


            mainFrame.Navigate(new HomePage(this));


   

        }
        
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioBar.Value = playMedia.Position.TotalSeconds ;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {


           
            var uri = new Uri("Test/Test.mp3", UriKind.Relative);
  
            if (trackState == "stop")
            {

            
            playMedia.Open(uri);
         
                playMedia.Play();
            timer.Start();
              


                Uri newImageUri = new Uri("Static/Images/Pause.png", UriKind.Relative);
            BitmapImage bitmapImage = new BitmapImage(newImageUri);
            
            Image image = new Image();
            image.Source = bitmapImage;
            image.Height = 20;
            image.Width = 20;
            playButton.Content = image;
            trackState = "play";
                

            }
           else if(trackState=="play")
            {
                Uri newImageUri = new Uri("Static/Images/Play.png", UriKind.Relative);
                BitmapImage bitmapImage = new BitmapImage(newImageUri);

                Image image = new Image();
                image.Source = bitmapImage;
                image.Height = 20;
                image.Width = 20;
                playButton.Content = image;

                playMedia.Pause();
                timer.Stop();
                trackState = "stop";
            }

        }

        

        //private void StopButton_Click(object sender, RoutedEventArgs e)
        //{
        //    mediaElement.Stop();
        //    timer.Stop();
            
        //}

        //private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        //{
        //    audioBar.IsEnabled = true;
        //    audioBar.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        //}

        //private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        //{
        //    StopButton_Click(sender, e);
        //}

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            
            mainFrame.Navigate(new HomePage(this));
          
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {

            mainFrame.Navigate(new AccountSettingsPage());
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            HelperMethods.OpenSignInWindow();
            
        }


        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (searchBar.Text!= string.Empty )
            {
                mainFrame.Navigate(new SearchPage(searchBar.Text));
            }
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
          

            mainFrame.Navigate(new AddTrackorPlaylist(this));
            
             
        }

        private void LikeButton_Click(Object sender, RoutedEventArgs e)
        {
            var result = ApiRequests.AddLikeToSong(Convert.ToInt32(likeImage.Tag)).Result;
            if (!result)
            {
                HelperMethods.ErrorMessage("Could not add Like, Please Try Again.");
                return;
            }
            Image image = new Image();
            image.Source = new BitmapImage(new Uri("Static/Images/RedHeart.png", UriKind.Relative));
            image.Height = 20;
            image.Width = 20;
            likeButton.Content = image;
        }
    }
}
