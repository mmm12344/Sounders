using MusicPlayerGUI.settings;
using Sounders.Views;
using System;
using System.Collections.Generic;
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
        private string pageState;
        private string trackState="stop";
        public MainWindow()
        {

           
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;


            if (Settings.GetUserID() == null || Settings.GetPassword() == null)
            {
                mainFrame.Navigate(new Uri("Views/signinPage.xaml", UriKind.Relative));
                pageState = "signin";
                return;
            }
            if (Settings.GetServerUrl() == null)
            {
                mainFrame.Navigate(new Uri("Views/AccountSettingsPage.xaml", UriKind.Relative));
                pageState = "setting";
                return;
            }



            mainFrame.Navigate(new Uri("Views/HomePage.xaml", UriKind.Relative));

            pageState = "home";



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
           

            
                mainFrame.Navigate(new Uri("Views/HomePage.xaml", UriKind.Relative));
            pageState = "home";

          
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            if (pageState != "settingp")
            {

                mainFrame.Navigate(new Uri("Views/AccountSettingsPage.xaml", UriKind.Relative));
                pageState = "settingp";
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            SigninSignUpWindow signinSignUpWindow = new SigninSignUpWindow();
            pageState="home";
            this.Close();
         
            signinSignUpWindow.Show();
            
        }

        private void searchBar_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            searchBar.Text= string.Empty;
          
        }

        private async void searchBar_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            
          await Task.Delay(100);
            searchBar.Text = "Search";
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if(pageState !="searchp")
            {
                if ( searchBar.Text!="Search" && searchBar.Text!= string.Empty )
                {
                    mainFrame.Navigate(new Uri("Views/SearchPage.xaml", UriKind.Relative));
                    pageState = "searchp";
                }
            }
        }

        private void uploadButton_Click(object sender, RoutedEventArgs e)
        {
          

                mainFrame.Navigate(new Uri("Views/AddTrackorPlaylist.xaml", UriKind.Relative));
            pageState = "upload";
             
        }
    }
}
