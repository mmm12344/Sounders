using MusicPlayerGUI;
using MusicPlayerGUI.settings;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Sounders
{
    class HelperMethods
    {
        public static BitmapImage GetBitmapImgFromBytes(byte[] bytes)
        {
            var songPicture = new BitmapImage();
            using (var ms = new System.IO.MemoryStream(bytes))
            {
                songPicture.BeginInit();
                songPicture.CacheOption = BitmapCacheOption.OnLoad; // here
                songPicture.StreamSource = ms;
                songPicture.EndInit();
            }
            return songPicture;
        }

        public static bool CheckIfSignedIn()
        {
            int? userID = Settings.GetUserID();
            string? password = Settings.GetPassword();
            if(userID != null && password != null)
            {
                return true;
            }
            return false;
        }

        public static void NavigateToHome(Frame frame)
        {
            frame.Navigate(new Uri("Views/HomePage.xaml", UriKind.Relative));
        }
        private static void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Hide();
        }

        public static void OpenMainWindow()
        {
            CloseAllWindows();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
        public static void OpenSignInWindow()
        {
            CloseAllWindows();
            SigninSignUpWindow signinSignUpWindow = new SigninSignUpWindow();
            signinSignUpWindow.Show();
        }

        public static void OpenSignInIfNotSigned()
        {
            if (!CheckIfSignedIn())
            {
                OpenSignInWindow();
            }
        }

        public static void ErrorMessage(string message)
        {
            MessageBox.Show(message, "Error");
        }

        public static void WarningMessage(string message)
        {
            MessageBox.Show(message, "Warning");
        }

        public static void SuccessMessage(string message)
        {
            MessageBox.Show(message, "Success");
        }
    }
}
