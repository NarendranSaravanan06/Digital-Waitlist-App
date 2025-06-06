using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using WaitListApp.Models;
using WaitListApp.Repositories;
using WaitListApp.Views;

namespace WaitListApp.Helpers
{
    public static class WaitlistActionsHelper
    {
        public static void EditEntry(ObservableCollection<WaitListModel> waitlist, WaitListModel selectedItem, Action reload)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an entry to edit.");
                return;
            }

            var popup = new InputPopup(selectedItem);
            if (popup.ShowDialog() == true)
            {
                var updatedEntry = new WaitListModel
                {
                    Id = selectedItem.Id,
                    Name = popup.UserName,
                    Email = popup.Email,
                    PhoneNo = popup.PhoneNo,
                    Status = popup.Status,
                    InTime = popup.InTime,
                    OutTime = popup.OutTime,
                    Date = popup.SelectedDate
                };

                new WaitlistRepository().Update(updatedEntry);
                reload();
                MessageBox.Show("Updated successfully!");
            }
        }

        public static void UpdateAll(ObservableCollection<WaitListModel> waitlist, Action reload)
        {
            if (waitlist == null || waitlist.Count == 0)
            {
                MessageBox.Show("No waitlist entries to update.");
                return;
            }
            foreach (var item in waitlist)
            {
                bool shouldUpdateOutTime = false;
                var updatedOutTime = item.OutTime;

                if ((item.OriginalStatus == "Waiting") &&
                (item.Status == "Completed" || item.Status == "Cancelled") &&
                item.Date.Date == DateTime.Now.Date)
                {
                    shouldUpdateOutTime = true;
                    updatedOutTime = DateTime.Now.TimeOfDay;
                }

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
            reload();
        }

        public static void DeleteEntry(ObservableCollection<WaitListModel> waitlist, WaitListModel selectedItem, Action reload)
        {
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
                reload();
                MessageBox.Show("Deleted successfully!");
            }
        }
        public static ObservableCollection<WaitListModel> LoadWaitlist(DateTime fromDate, DateTime toDate)
        {
            var repo = new WaitlistRepository();
            var entries = repo.GetByDateRange(fromDate, toDate);

            return new ObservableCollection<WaitListModel>(
                entries.Select((entry, index) =>
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
        }
    }
}
