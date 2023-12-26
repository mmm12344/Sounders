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
    /// Interaction logic for SigninPage.xaml
    /// </summary>
    public partial class SigninPage : Page
    {
        public SigninPage()
        {
            InitializeComponent();
        }


        public void signinButton_Click(object sender, RoutedEventArgs e)
        {
            string password = passwordTextBox.Password.ToString();
            string email = emailTextBox.Text;

            if(email.Length <= 5) 
            {
                HelperMethods.WarningMessage("Email is Incorrect, Please Try Again.");
                return;
            }
            if(password.Length < 8)
            {
                HelperMethods.WarningMessage("Password is less than 8 Characters, Please Try Again.");
                return;
            }
            Task<int> userID = ApiRequests.SignIn(new UserSignIn(email, password));
            if(userID.Result == -1)
            {
                HelperMethods.WarningMessage("Email or Password is incorrect, Please Try Again.");
                return;
            }
            Settings.UpdateUserID(userID.Result);
            Settings.UpdatePassword(password);
            ApiRequests.UpdateClient();
            HelperMethods.SuccessMessage("Signed In Successfully!");
            HelperMethods.OpenMainWindow();
        }
    }
}
