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

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var repo = new UserRepository();
            if (repo.IsRegistered(userName.Text, password.Password))
            {
                MessageBox.Show("hello " + userName.Text);
                Dashboard dashboard = new Dashboard();
                dashboard.Show();
                Close();

            }
            else {
                MessageBox.Show("wrong login");
            }
        }
    }
}
