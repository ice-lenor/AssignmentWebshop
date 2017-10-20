using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace AssignmentWebshop.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; } // unique autoincrement key

        public String ProductName { get; set; } // maps to "Key"
        public String ArticleCode { get; set; } // maps to "Artikelcode"

        public int? ProductTypeId { get; set; }
        public virtual ProductType ProductType { get; set; } // maps to "colorcode" - why "color" at all?

        public int? ManufacturerId { get; set; }
        public virtual Manufacturer Manufacturer { get; set; } // maps to "description" - again, not sure

        [Column(TypeName = "Money")]
        public Decimal Price { get; set; } // what units?

        [Column(TypeName = "Money")]
        public Decimal? DiscountPrice { get; set; } // what units?

        public int? DeliveryRangeId { get; set; }
        public virtual DeliveryRange DeliveryRange { get; set; } // maps to "delivered in"

        public int? PersonTypeId { get; set; }
        public virtual PersonType PersonType { get; set; } // maps to "q1"

        public int? SizeId { get; set; }
        public virtual Size Size { get; set; } // maps to "size"

        public int? ColorId { get; set; }
        public virtual Color Color { get; set; } // maps to "color"
    }

    public class ProductType
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Manufacturer
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class DeliveryRange
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class PersonType // bad name
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Size
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class Color
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
    }

    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<DeliveryRange> DeliveryRanges { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }
    }
}