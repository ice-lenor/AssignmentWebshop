using System;

namespace AssignmentWebshop.ProductImport
{
    /// <summary>
    /// Product object in a format that comes directly from the user input.
    /// </summary>
    public class ProductRaw
    {
        public String ProductName { get; set; }
        public String ArticleCode { get; set; }
        public String ProductType { get; set; }
        public String Manufacturer { get; set; }
        public String Price { get; set; }
        public String DiscountPrice { get; set; }
        public String DeliveryRange { get; set; }
        public String PersonType { get; set; }
        public String Size { get; set; }
        public String Color { get; set; }
    }
}