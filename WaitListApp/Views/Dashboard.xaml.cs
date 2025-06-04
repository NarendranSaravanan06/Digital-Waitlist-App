using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WaitListApp.Helpers;
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
            LoadTodayData();
        }

        private void LoadTodayData()
        {
            var repo = new WaitlistRepository();
            var todayList = repo.GetTodayEntries();

            waitlist = new ObservableCollection<WaitListModel>(
                todayList.Select((entry, index) =>
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

        private void Refresh_click(object sender, RoutedEventArgs e) => LoadTodayData();

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
                LoadTodayData();
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgWaitlist.SelectedItem as WaitListModel;
            WaitlistActionsHelper.EditEntry(waitlist, selected, LoadTodayData);
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            WaitlistActionsHelper.UpdateAll(waitlist, LoadTodayData);
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = dgWaitlist.SelectedItem as WaitListModel;
            WaitlistActionsHelper.DeleteEntry(waitlist, selectedItem, LoadTodayData);
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
