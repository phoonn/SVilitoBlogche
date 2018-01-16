using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IdentityMVC.Models
{
    public class PagedListModel<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageCount { get; set; }

        public PagedListModel(IEnumerable<T> items, int totalCount, int pageCount)
        {
            Items = items;
            TotalCount = totalCount;
            PageCount = pageCount;
        }
    }
}