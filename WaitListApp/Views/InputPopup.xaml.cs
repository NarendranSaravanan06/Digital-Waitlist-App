using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaitListApp.Models;

namespace WaitListApp.Views
{
    public partial class InputPopup : Window
    {
        public string UserName => txtName.Text;
        public string Email => txtEmail.Text;
        public string PhoneNo => txtPhNo.Text;
        public string Status => (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Waiting";

        public InputPopup(WaitListModel existingEntry = null)
        {
            InitializeComponent();

            if (existingEntry != null)
            {
                txtName.Text = existingEntry.Name;
                txtEmail.Text = existingEntry.Email;
                txtPhNo.Text = existingEntry.PhoneNo;

                foreach (ComboBoxItem item in cbStatus.Items)
                {
                    if (item.Content.ToString() == existingEntry.Status)
                    {
                        cbStatus.SelectedItem = item;
                        break;
                    }
                }

                Title = "Update Entry";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text.Trim();
            string email = txtEmail.Text.Trim();
            string phone = txtPhNo.Text.Trim();
            string status = (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString();

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Name is required."); txtName.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(email) || !email.Contains("@") || !email.Contains("."))
            {
                MessageBox.Show("Valid email is required."); txtEmail.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(phone) || phone.Length != 10 || !phone.All(char.IsDigit))
            {
                MessageBox.Show("Valid phone number is required."); txtPhNo.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(status))
            {
                MessageBox.Show("Please select a status."); cbStatus.Focus(); return;
            }

            DialogResult = true;
        }
    }
}
