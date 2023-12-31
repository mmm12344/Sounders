using MusicPlayerGUI;
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
        MainWindow mainWindow;
        public AccountSettingsPage(MainWindow mainWindow)
        {
            InitializeComponent();



            this.mainWindow = mainWindow;

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
                ApiRequests.UpdateClient();
            }

            string firstName = changeFirstNameTextBox.Text;
            string lastName = changeLastNameTextBox.Text;
            string password = chanePasswordPasswordBox.Password.ToString();
            if (firstName.Length != 0 && lastName.Length != 0 && password.Length != 0)
            {
                if (password.Length != 0 && password.Length < 8)
                {
                    HelperMethods.ErrorMessage("Password is less than 8 Characters, Please Try Again.");
                    return;
                }

                UserSignUp user = new UserSignUp((firstName.Length > 0) ? firstName : null, (lastName.Length > 0) ? lastName : null, null, (password.Length > 0) ? password : null);
                bool result = ApiRequests.ChangeUserInfo(user).Result;
                if (result)
                {
                    HelperMethods.SuccessMessage("User Info Changed!");
                    mainWindow.mainFrame.Navigate(new HomePage(mainWindow));
                }
                else
                    HelperMethods.ErrorMessage("Could not Change user Info, Please Try Again.");
            }

        }
    }
}
