// HistoryViewer.xaml.cs
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaitListApp.Helpers;
using WaitListApp.Models;
using WaitListApp.Repositories;

namespace WaitListApp.Views
{
    public partial class HistoryViewer : Window
    {
        private ObservableCollection<WaitListModel> waitlist;

        public HistoryViewer()
        {
            InitializeComponent();
            dateFrom.SelectedDate = DateTime.Today;
            dateTo.SelectedDate = DateTime.Today;
            LoadData(DateTime.Today, DateTime.Today);
        }

        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            waitlist = WaitlistActionsHelper.LoadWaitlist(fromDate, toDate);
            dgHistory.ItemsSource = waitlist;
        }


        private void Fetch_Click(object sender, RoutedEventArgs e)
        {
            if (dateFrom.SelectedDate == null || dateTo.SelectedDate == null)
            {
                MessageBox.Show("Please select both From and To dates.");
                return;
            }
            LoadData(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value);
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgHistory.SelectedItem as WaitListModel;
            WaitlistActionsHelper.EditEntry(waitlist, selected, () => LoadData(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value));
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            WaitlistActionsHelper.UpdateAll(waitlist, () => LoadData(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var selected = dgHistory.SelectedItem as WaitListModel;
            WaitlistActionsHelper.DeleteEntry(waitlist, selected, () => LoadData(dateFrom.SelectedDate.Value, dateTo.SelectedDate.Value));
        }

        private void Close_Click(object sender, RoutedEventArgs e) => Close();

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            var dashboard = new Dashboard();
            dashboard.Show();
            this.Close();
        }

    }
}
