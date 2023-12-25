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
    /// Interaction logic for SignupPage.xaml
    /// </summary>
    public partial class SignupPage : Page
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        public void signup_Click(object sender, RoutedEventArgs e)
        {
            if(passwordTextBox.Password.ToString() == reEnterPasswordTextBox.ToString())
            {
                MessageBox.Show("Password doesn't match", "Alert");
                return;
            }
            string password = passwordTextBox.Password.ToString();
            string email = emailTextBox.Text;
            string firstName = firstNameTextBox.Text;
            string lastName = lastNameTextBox.Text;

            if(email.Length <= 5)
            {
                MessageBox.Show("email is inCorrect", "Alert");
                return;
            }
            if(firstName.Length == 0 || lastName.Length == 0) 
            {

                MessageBox.Show("name is inCorrect", "Alert");
                return;

            }
            if(password.Length < 8)
            {
                MessageBox.Show("Password is less than 8 Characters");
                return;
            }

            Task<int> userID = ApiRequests.SignUp(new UserSignUp(firstName, lastName, email, password));
            if(userID.Result == -1) 
            {
                MessageBox.Show("Error, Please Try Again", "Alert");
                return;
            }
            Settings.UpdateUserID(userID.Result);
            Settings.UpdatePassword(password);
            ApiRequests.UpdateClient();
        }
    }
}
