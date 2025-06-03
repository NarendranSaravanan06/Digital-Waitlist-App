using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WaitListApp.Data;
using WaitListApp.Models;
using WaitListApp.Repositories;

namespace WaitListApp.Views // <---- Must match x:Class in Dashboard.xaml
{
    public partial class Dashboard : Window
    {
        private ObservableCollection<WaitListModel> waitlist;

        public Dashboard()
        {
            InitializeComponent(); // XAML wiring
            LoadData();
        }

        private void LoadData()
        {
            var repo = new WaitlistRepository();
            var data = repo.GetAll();

            waitlist = new ObservableCollection<WaitListModel>(
    data.Select((entry, index) => {
        var model = new WaitListModel
        {
            SNo = index + 1,
            Id = entry.Id,
            Name = entry.Name,
            Email = entry.Email,
            PhoneNo = entry.PhoneNo,
            Status = entry.Status
        };
        model.SetOriginalValues(); // Track initial DB state
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
                    Status = inputPopup.Status
                };

                new WaitlistRepository().Add(newEntry);
                LoadData();
            }
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
                    Status = popup.Status
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
                var updatedEntry = new WaitListModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    Email = item.Email,
                    PhoneNo = item.PhoneNo,
                    Status = item.Status
                };

                new WaitlistRepository().Update(updatedEntry);
            }

            MessageBox.Show("Updated successfully!");
            LoadData();
        }
    
    }
}
    