using MusicPlayerGUI.settings;
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
    /// Interaction logic for AccountSettingsPage.xaml
    /// </summary>
    public partial class AccountSettingsPage : Page
    {
        public AccountSettingsPage()
        {
            InitializeComponent();
            string serverUrl = Settings.GetServerUrl();
            if (serverUrl != null)
            {
                serverUrlTextBox.Text = serverUrl;
            }
        }

        public void applyChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string serverUrl = serverUrlTextBox.Text;
            if(serverUrl.Length > 7)
            {
                Settings.UpdateServerUrl(serverUrl);
            }
        }
    }
}
