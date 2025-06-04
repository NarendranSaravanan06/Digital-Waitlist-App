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
        private Paginator<WaitListModel> paginator;

        public Dashboard()
        {
            InitializeComponent();
            LoadTodayData();
        }

       
        private void LoadTodayData()
        {
            var rawList = WaitlistActionsHelper.LoadWaitlist(DateTime.Today, DateTime.Today).ToList();
            paginator = new Paginator<WaitListModel>(rawList);
            dgWaitlist.ItemsSource = paginator.PagedItems;
            UpdatePageInfo();

        }
        private void UpdatePageInfo()
        {
            txtPageInfo.Text = $"Page {paginator?.CurrentPage ?? 0} of {paginator?.TotalPages ?? 0}";
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

        private void ViewHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new HistoryViewer();
            historyWindow.Show();
            this.Hide(); // Optional: Hide dashboard while history is open
        }
        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            paginator?.PreviousPage();
            UpdatePageInfo();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            paginator?.NextPage();
            UpdatePageInfo();
        }

    }
}
