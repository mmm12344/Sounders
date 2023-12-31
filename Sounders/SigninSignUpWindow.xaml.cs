﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;

namespace Sounders
{
    /// <summary>
    /// Interaction logic for SigninSignUpWindow.xaml
    /// </summary>
    public partial class SigninSignUpWindow : Window
    {

        void MainWindow_Closing(object sender, CancelEventArgs e)
        {
            HelperMethods.CloseAllWindows();
        }

        public SigninSignUpWindow()
        {
            InitializeComponent();
            signInsignUp.Navigate(new Uri("Views/SigninPage.xaml", UriKind.Relative));
        }
    }
}
