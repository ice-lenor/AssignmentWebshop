using System.Collections.Generic;

namespace AssignmentWebshop.Models
{
    /// <summary>
    /// Model for the "see all products page"
    /// </summary>
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

        /// <summary>
        /// List of products displayed on the current page
        /// </summary>
        public ICollection<Product> CurrentProducts { get; private set; }

        /// <summary>
        /// Current page number
        /// </summary>
        public int PageNumber { get; private set; }

        /// <summary>
        /// Count of all pages
        /// </summary>
        public int PagesTotalCount { get; private set; }

        /// <summary>
        /// Total number of all products
        /// </summary>
        public int ProductsTotalCount { get; private set; }

        /// <summary>
        /// Product index - link to go to previous page.
        /// If null, then there is no previous page.
        /// </summary>
        public int? PreviousPageProductIndex { get; set; }

        /// <summary>
        /// Product index - link to go to next page.
        /// If null, then there is no next page.
        /// </summary>
        public int? NextPageProductIndex { get; set; }
    }
}