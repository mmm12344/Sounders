using Microsoft.Win32;
using MusicPlayerGUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Sounders.Views
{
    /// <summary>
    /// Interaction logic for AddTrackPage.xaml
    /// </summary>
    public partial class AddTrackPage : Page
    {

        private BitmapImage songPic = null;
        private string fileLocation = null;
        public AddTrackPage()
        {
            InitializeComponent();
        }

        private void browseTracksButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            fileDialog.Filter = "Audio Files|*.mp3;*.wav;";
            Nullable<bool> result = fileDialog.ShowDialog();

            if (result == true)
            {
                fileLocation = fileDialog.FileName;
                fileLocationLabel.Content = fileDialog.FileName;
            }
        }

        private void browsePicture_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image Files|*.png;*.jpeg;";
            Nullable<bool> result = fileDialog.ShowDialog();



            if (result == true)
            {
                Image img = new Image();
                songPic = new BitmapImage(new Uri(fileDialog.FileName));
                img.Source = songPic;
                browsePicture.Content = img;

            }
        }

        private void UploadButton_Click(Object sender, RoutedEventArgs e)
        {
            string songName = songNameTextBox.Text;
            
            if(songPic != null && fileLocation != null && songName.Length > 0)
            {
                byte[] songPicData;
                byte[] songData;
                try
                {
                    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(songPic));
                    using (MemoryStream ms = new MemoryStream())
                    {
                        encoder.Save(ms);
                        songPicData = ms.ToArray();
                    }
                    songData = File.ReadAllBytes(fileLocation);

                }
                catch
                {
                    HelperMethods.ErrorMessage("An Error has Occuried, Please Try Again.");
                    return;
                }
                var result = ApiRequests.AddSong(new PostSong(songName, songData, songPicData)).Result;
                if(result == false)
                {
                    HelperMethods.ErrorMessage("Could not uplaod Content, Please Try Again");
                    return;
                }
                HelperMethods.SuccessMessage("Posted Song Successfully!");
                
            }
        }
    }
}
