using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaitListApp.Data;
using WaitListApp.Models;
using WaitListApp.Repositories;

namespace WaitListApp.Views
{
    public partial class Dashboard : Window
    {
        private ObservableCollection<WaitListModel> waitlist;

        public Dashboard()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var repo = new WaitlistRepository();
            var data = repo.GetAll();

            waitlist = new ObservableCollection<WaitListModel>(
                data.Select((entry, index) =>
                {
                    var model = new WaitListModel
                    {
                        SNo = index + 1,
                        Id = entry.Id,
                        Name = entry.Name,
                        Email = entry.Email,
                        PhoneNo = entry.PhoneNo,
                        Status = entry.Status,
                        InTime = entry.InTime,
                        OutTime = entry.OutTime,
                        Date = entry.Date
                    };
                    model.SetOriginalValues();
                    return model;
                }));

            dgWaitlist.ItemsSource = waitlist;
        }

        private void Refresh_click(object sender, RoutedEventArgs e) => LoadData();

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            var inputPopup = new InputPopup();
            if (inputPopup.ShowDialog() == true)
            {
                var newEntry = new WaitListModel
                {
                    Name = inputPopup.UserName,
                    Email = inputPopup.Email,
                    PhoneNo = inputPopup.PhoneNo,
                    Status = inputPopup.Status,
                    InTime = inputPopup.InTime,
                    OutTime = inputPopup.OutTime,
                    Date = inputPopup.SelectedDate
                };

                new WaitlistRepository().Add(newEntry);
                LoadData();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgWaitlist.SelectedItem as WaitListModel;
            if (selected == null)
            {
                MessageBox.Show("Please select an entry to edit.");
                return;
            }

            var popup = new InputPopup(selected); // Pass existing data
            if (popup.ShowDialog() == true)
            {
                var updatedEntry = new WaitListModel
                {
                    Id = selected.Id,
                    Name = popup.UserName,
                    Email = popup.Email,
                    PhoneNo = popup.PhoneNo,
                    Status = popup.Status,
                    InTime = popup.InTime,
                    OutTime = popup.OutTime,
                    Date = popup.SelectedDate
                };

                new WaitlistRepository().Update(updatedEntry);
                LoadData();
                MessageBox.Show("Updated successfully!");
            }
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in waitlist)
            {
                var shouldUpdateOutTime = false;
                var updatedOutTime = item.OutTime;

                // Case 1: Status changed from Waiting → Completed/Cancelled
                if ((item.OriginalStatus == "Waiting") &&
                    (item.Status == "Completed" || item.Status == "Cancelled"))
                {
                    shouldUpdateOutTime = true;
                    updatedOutTime = DateTime.Now.TimeOfDay;
                }
                // Case 2: Manual change in OutTime (by comparing original value)
                else if (item.OutTime != item.OriginalOutTime)
                {
                    shouldUpdateOutTime = true;
                }

                var updatedEntry = new WaitListModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    PhoneNo = item.PhoneNo,
                    Status = item.Status,
                    InTime = item.InTime,
                    Date = item.Date,
                    OutTime = shouldUpdateOutTime ? updatedOutTime : item.OriginalOutTime
                };

                new WaitlistRepository().Update(updatedEntry);
            }

            MessageBox.Show("Updated successfully!");
            LoadData();
        }


        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgWaitlist.SelectedItem as WaitListModel;
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an entry to delete.");
                return;
            }

            var confirm = MessageBox.Show(
                $"Are you sure you want to delete:\n\n{selectedItem.Name} ({selectedItem.Email})?",
                "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (confirm == MessageBoxResult.Yes)
            {
                new WaitlistRepository().Delete(selectedItem.Id);
                LoadData();
                MessageBox.Show("Deleted successfully!");
            }
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var repo = new UserRepository();
            repo.logout("admin");
            var login = new Login();
            login.Show();
            Close();
        }
    }
}
