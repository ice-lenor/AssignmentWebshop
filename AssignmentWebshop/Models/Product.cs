using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssignmentWebshop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public String ProductName { get; set; } // maps to "Key"
        public String ArticleCode { get; set; } // maps to "Artikelcode"

        public ProductType ProductType { get; set; } // maps to "colorcode" - why "color" at all?
        public Manufacturer Manufacturer { get; set; } // maps to "description" - again, not sure
        
        public Decimal Price { get; set; } // what units?
        public Decimal DiscountPrice { get; set; }

        public DeliveryRange DeliveryRange { get; set; } // maps to "delivered in"

        public PersonType PersonType { get; set; } // maps to "q1"

        public Size Size { get; set; } // maps to "size"
        public Color Color { get; set; } // maps to "color"
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }

    public class ProductType
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Manufacturer
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class DeliveryRange
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class PersonType // bad name
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Size
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Color
    {
        public int Id { get; set; }
        public String Name { get; set; }
    }
}