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
using System.Windows.Shapes;
using MaterialDesignThemes.Wpf;
using WaitListApp.Data;
using WaitListApp.Repositories;
namespace WaitListApp.Views
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            var repo = new UserRepository();
            if (repo.isloggedIn())
            {
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var repo = new UserRepository();
            string username = userName.Text.Trim();
            string pwd = password.Password;

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(pwd))
            {
                MessageBox.Show("Please enter both username and password.", "Missing Fields", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!repo.UserExists(username))
            {
                MessageBox.Show("Username does not exist.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!repo.IsPasswordCorrect(username, pwd))
            {
                MessageBox.Show("Incorrect password. Please try again.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Successful login
            repo.IsRegistered(username, pwd); // sets isloggedin true + timer
            MessageBox.Show($"Welcome, {username}!", "Login Successful", MessageBoxButton.OK, MessageBoxImage.Information);

            Dashboard dashboard = new Dashboard();
            dashboard.Show();
            Close();
        }

    }
}
