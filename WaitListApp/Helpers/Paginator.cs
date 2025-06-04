using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WaitListApp.Helpers
{
    public class Paginator<T>
    {
        private readonly IList<T> allItems;
        public ObservableCollection<T> PagedItems { get; private set; }

        public int PageSize { get; }
        public int CurrentPage { get; private set; } = 1;
        public int TotalPages => (int)Math.Ceiling((double)allItems.Count / PageSize);

        public Paginator(IEnumerable<T> items, int pageSize = 30)
        {
            allItems = items.ToList();
            PageSize = pageSize;
            PagedItems = new ObservableCollection<T>();
            LoadPage(1);
        }

        public void LoadPage(int pageNumber)
        {
            if (pageNumber < 1 || pageNumber > TotalPages) return;
            CurrentPage = pageNumber;
            var pageItems = allItems
                .Skip((CurrentPage - 1) * PageSize)
                .Take(PageSize)
                .ToList();

            PagedItems.Clear();
            foreach (var item in pageItems)
                PagedItems.Add(item);
        }

        public void NextPage() => LoadPage(CurrentPage + 1);
        public void PreviousPage() => LoadPage(CurrentPage - 1);
    }
}
