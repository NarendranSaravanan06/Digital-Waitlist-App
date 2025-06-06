using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaitListApp.Models;

namespace WaitListApp.Views
{
    public partial class InputPopup : Window
    {
        public string UserName => txtName.Text.Trim();
        public string Email => txtEmail.Text.Trim();
        public string PhoneNo => txtPhNo.Text.Trim();
        public string Status => (cbStatus.SelectedItem as ComboBoxItem)?.Content.ToString() ?? "Waiting";
        public TimeSpan InTime => TimeSpan.TryParse(txtInTime.Text, out var t) ? t : DateTime.Now.TimeOfDay;
        public TimeSpan? OutTime => TimeSpan.TryParse(txtOutTime.Text, out var t) ? t : (TimeSpan?)null;
        public DateTime SelectedDate => dpDate.SelectedDate ?? DateTime.Today;

        public InputPopup(WaitListModel existingEntry = null)
        {
            InitializeComponent();

            txtInTime.Text = DateTime.Now.ToString("HH:mm");
            dpDate.SelectedDate = DateTime.Today;

            if (existingEntry != null)
            {
                txtName.Text = existingEntry.Name;
                txtEmail.Text = existingEntry.Email;
                txtPhNo.Text = existingEntry.PhoneNo;
                txtInTime.Text = existingEntry.InTime.ToString(@"hh\:mm");
                txtOutTime.Text = existingEntry.OutTime?.ToString(@"hh\:mm") ?? "";
                dpDate.SelectedDate = existingEntry.Date;

                foreach (ComboBoxItem item in cbStatus.Items)
                {
                    if (item.Content.ToString() == existingEntry.Status)
                    {
                        cbStatus.SelectedItem = item;
                        break;
                    }
                }

                Title = "Update Entry";
                btnSubmit.Content = "Update";
            }
            else
            {
                btnSubmit.Content = "Register";
                cbStatus.SelectedIndex = 0;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Auto-correct common format mistakes
            txtInTime.Text = txtInTime.Text.Replace('.', ':');
            txtOutTime.Text = txtOutTime.Text.Replace('.', ':');

            if (string.IsNullOrWhiteSpace(UserName))
            {
                MessageBox.Show("Name is required."); txtName.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(Email) || !Email.Contains("@") || !Email.Contains("."))
            {
                MessageBox.Show("Valid email is required."); txtEmail.Focus(); return;
            }

            if (string.IsNullOrWhiteSpace(PhoneNo) || PhoneNo.Length != 10 || !PhoneNo.All(char.IsDigit))
            {
                MessageBox.Show("Valid phone number is required."); txtPhNo.Focus(); return;
            }

            if (!TimeSpan.TryParse(txtInTime.Text, out _))
            {
                MessageBox.Show("Enter valid In Time (HH:mm)."); txtInTime.Focus(); return;
            }

            if (!string.IsNullOrWhiteSpace(txtOutTime.Text) && !TimeSpan.TryParse(txtOutTime.Text, out _))
            {
                MessageBox.Show("Enter valid Out Time (HH:mm) or leave it empty."); txtOutTime.Focus(); return;
            }

            if (dpDate.SelectedDate == null)
            {
                MessageBox.Show("Please select a valid date."); dpDate.Focus(); return;
            }

            DialogResult = true;
        }
    }
}
