﻿using Microsoft.Win32;
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
    /// Interaction logic for AddPlayListPage.xaml
    /// </summary>
    public partial class AddPlayListPage : Page
    {
        private BitmapImage playListPic = null;

        
        public AddPlayListPage()
        {
            InitializeComponent();

            List<SongInfo> result = ApiRequests.GetOwnSongs().Result;
            if (result != null)
            {

                try
                {
                    foreach (SongInfo song in result)
                    {
                        var songPicture = HelperMethods.GetBitmapImgFromBytes(song.picture);
                        var toAdd = new { songName = song.name, songPic = songPicture };
                        //AddedTracksList.Items.Add(toAdd);   this code was related to addedtracks list that i removed
                    }
                }
                catch
                {
                    HelperMethods.ErrorMessage("Error, Please Try Again.");
                    return;
                }
            }

        }

        private void browsePictureButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Filter = "Image Files|*.png;*.jpeg;";
            Nullable<bool> result = fileDialog.ShowDialog();



            if (result == true)
            {
                Image img = new Image();
                playListPic = new BitmapImage(new Uri(fileDialog.FileName));
                img.Source = playListPic;
                browsePictureButton.Content = img;

            }
        }
    }
}
