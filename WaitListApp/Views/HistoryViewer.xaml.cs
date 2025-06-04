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
        private Paginator<WaitListModel> paginator;

        public HistoryViewer()
        {
            InitializeComponent();
            dateFrom.SelectedDate = DateTime.Today;
            dateTo.SelectedDate = DateTime.Today;
            LoadData(DateTime.Today, DateTime.Today);
        }

        private void LoadData(DateTime fromDate, DateTime toDate)
        {
            var rawList = WaitlistActionsHelper.LoadWaitlist(fromDate, toDate).ToList();
            paginator = new Paginator<WaitListModel>(rawList);
            dgHistory.ItemsSource = paginator.PagedItems;
            UpdatePageInfo();
        }
        private void UpdatePageInfo()
        {
            txtPageInfo.Text = $"Page {paginator?.CurrentPage ?? 0} of {paginator?.TotalPages ?? 0}";
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
