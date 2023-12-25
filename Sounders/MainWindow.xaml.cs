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
        private string state;
        public MainWindow()
        {

           
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;


            mainFrame.Navigate(new Uri("Views/HomePage.xaml", UriKind.Relative));

            state = "home";
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            audioBar.Value = playMedia.Position.TotalSeconds ;
        }

        private void PlayButton_Click(object sender, RoutedEventArgs e)
        {
           
            
           
            var uri = new Uri(@"C:\Users\pc\Source\Repos\mmm12344\Sounders\Sounders\Test\Test.mp3",UriKind.Relative); 
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

        }

        private void PauseButton_Click(object sender, RoutedEventArgs e)
        {
            playMedia.Pause();
            timer.Stop();
        }

        private void StopButton_Click(object sender, RoutedEventArgs e)
        {
            mediaElement.Stop();
            timer.Stop();
            audioBar.Value = 0;
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            audioBar.IsEnabled = true;
            audioBar.Maximum = mediaElement.NaturalDuration.TimeSpan.TotalSeconds;
        }

        private void MediaElement_MediaEnded(object sender, RoutedEventArgs e)
        {
            StopButton_Click(sender, e);
        }

        private void homeButton_Click(object sender, RoutedEventArgs e)
        {
            if(state == "home")
            {

            }
            else
            {
                mainFrame.Navigate(new Uri("Views/HomePage.xaml", UriKind.Relative));

                state = "home";
            }
        }

        private void settingButton_Click(object sender, RoutedEventArgs e)
        {
            if (state == "setting")
            {

            }
            else
            {
                mainFrame.Navigate(new Uri("Views/AccountSettingsPage.xaml", UriKind.Relative));
                state = "setting";
            }
        }

        private void logoutButton_Click(object sender, RoutedEventArgs e)
        {
            SigninSignUpWindow signinSignUpWindow = new SigninSignUpWindow();
            Close();
         
            signinSignUpWindow.Show();
        }
    }
}
