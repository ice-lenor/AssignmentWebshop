using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace AssignmentWebshop.Models
{
    /// <summary>
    /// The ORM object of the product
    /// </summary>
    public class Product
    {
        [Key]
        public int Id { get; set; } // unique autoincrement key

        /// <summary>
        /// Name of the product
        /// </summary>
        [StringLength(150)]
        public String ProductName { get; set; } // maps to "Key"

        /// <summary>
        /// Article code of the product
        /// </summary>
        [StringLength(150)]
        public String ArticleCode { get; set; } // maps to "Artikelcode"

        /// <summary>
        /// Type of the product
        /// </summary>
        public int? ProductTypeId { get; set; }

        /// <summary>
        /// Type of the product
        /// </summary>
        public virtual ProductType ProductType { get; set; } // maps to "colorcode" - why "color" at all here?

        /// <summary>
        /// Manufacturer of the product
        /// </summary>
        public int? ManufacturerId { get; set; }

        /// <summary>
        /// Manufacturer of the product
        /// </summary>
        public virtual Manufacturer Manufacturer { get; set; } // maps to "description" - again, not sure

        /// <summary>
        /// Price of the product
        /// </summary>
        [Column(TypeName = "Money")]
        public Decimal Price { get; set; } // what units?

        /// <summary>
        /// Discount price of the product
        /// </summary>
        [Column(TypeName = "Money")]
        public Decimal? DiscountPrice { get; set; } // what units?

        /// <summary>
        /// Estimated delivery time of the product
        /// </summary>
        public int? DeliveryRangeId { get; set; }

        /// <summary>
        /// Estimated delivery time of the product
        /// </summary>
        public virtual DeliveryRange DeliveryRange { get; set; } // maps to "delivered in"

        /// <summary>
        /// Type of person who this product is for. For example: baby, toddler, girl 7-12, etc.
        /// </summary>
        public int? PersonTypeId { get; set; }
        /// <summary>
        /// Type of person who this product is for. For example: baby, toddler, girl 7-12, etc.
        /// </summary>
        public virtual PersonType PersonType { get; set; } // maps to "q1"

        /// <summary>
        /// Size of the product
        /// </summary>
        public int? SizeId { get; set; }

        /// <summary>
        /// Size of the product
        /// </summary>
        public virtual Size Size { get; set; } // maps to "size"

        /// <summary>
        /// Color of the product
        /// </summary>
        public int? ColorId { get; set; }

        /// <summary>
        /// Color of the product
        /// </summary>
        public virtual Color Color { get; set; } // maps to "color"
    }

    /// <summary>
    /// A dictionary: a collection of values that can be referenced to.
    /// For example, a list of colors, or a list of clothing sizes.
    /// </summary>
    public interface INamedDictionary
    {
        int Id { get; }
        String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: types of the product
    /// </summary>
    public class ProductType : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: manufacturers of the product
    /// </summary>
    public class Manufacturer : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: estimated ranges of delivery dates
    /// </summary>
    public class DeliveryRange : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: possible types of person who this product is for. For example: baby, toddler, girl 7-12, etc.
    /// </summary>
    public class PersonType : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: possible sizes of the product
    /// </summary>
    public class Size : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
        public String Name { get; set; }
    }

    /// <summary>
    /// Dictionary: possible colors of the product
    /// </summary>
    public class Color : INamedDictionary
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        [Index(IsUnique = true)]
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