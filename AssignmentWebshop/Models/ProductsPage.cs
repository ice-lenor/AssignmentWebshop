using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentWebshop.Models
{
    public class ProductsPage
    {
        public ProductsPage(ICollection<Product> products,
            int pageNumber, int pagesTotalCount, int productsTotalCount)
        {
            CurrentProducts = products;
            PageNumber = pageNumber;
            PagesTotalCount = pagesTotalCount;
            ProductsTotalCount = productsTotalCount;
        }
        public ICollection<Product> CurrentProducts { get; private set; }
        public int PageNumber { get; private set; }
        public int PagesTotalCount { get; private set; }
        public int ProductsTotalCount { get; private set; }

        public int? PreviousPageProductIndex { get; set; }
        public int? NextPageProductIndex { get; set; }
    }
}