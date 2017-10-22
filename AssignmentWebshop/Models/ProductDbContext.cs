using System;
using System.Data.Entity;

namespace AssignmentWebshop.Models
{
    public class ProductDBContext : DbContext
    {
        public DbSet<Product> Products { get; set; }

        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<DeliveryRange> DeliveryRanges { get; set; }
        public DbSet<PersonType> PersonTypes { get; set; }
        public DbSet<Size> Sizes { get; set; }
        public DbSet<Color> Colors { get; set; }



        public IDbSet<T> GetDbSetByItemType<T>()
            where T : class, INamedDictionary, new()
        {
            String typeName = typeof(T).Name;

            if (typeName == typeof(ProductType).Name)
                return (IDbSet<T>)ProductTypes;

            if (typeName == typeof(Manufacturer).Name)
                return (IDbSet<T>)Manufacturers;

            if (typeName == typeof(DeliveryRange).Name)
                return (IDbSet<T>)DeliveryRanges;

            if (typeName == typeof(PersonType).Name)
                return (IDbSet<T>)PersonTypes;

            if (typeName == typeof(Size).Name)
                return (IDbSet<T>)Sizes;

            if (typeName == typeof(Color).Name)
                return (IDbSet<T>)Colors;

            return null;
        }
    }
}