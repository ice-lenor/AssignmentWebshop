using AssignmentWebshop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace AssignmentWebshop.ProductImport
{
    /// <summary>
    /// Converts the product from a <see cref="ProductRaw"/> raw value, coming from user input,
    /// to a valid <see cref="Product"/> product object.
    /// </summary>
    public class ProductRawToProductConverter
    {
        ProductDBContext m_db;
        Dictionary<String, Dictionary<String, int>> m_dictionaryValues;

        public ProductRawToProductConverter(ProductDBContext db)
        {
            m_db = db;
            m_dictionaryValues = new Dictionary<string, Dictionary<string, int>>();
        }

        /// <summary>
        /// Converts the product from a raw value, coming from user input,
        /// to a valid product object.
        /// </summary>
        /// <param name="productRaw">Raw product, coming from the user</param>
        /// <returns>Product as a DB object.</returns>
        public Product Convert(ProductRaw productRaw)
        {
            // In a real production system here could be some validation:
            // for example, that the delivery dates are valid;
            // that all dictionary entries already exist;
            // or that the article code is always set up and comes in a given format.

            // In a real production system, we could, for example:
            // - log an error and fire an alert for the user
            // - log to a special table "parsing problems" and offer to the user to fix all problems afterwards
            // - or offer the user to fix the error right away;
            // - skip the whole entry
            // - or save the whole entry without the errorneous field (like we do now).

            // Because it is a "test exercise",
            // and because there's almost no information about the customer, their domain area, and the data itself,
            // we validate almost nothing.

            Product result = new Product();

            result.ProductName = productRaw.ProductName;

            result.ArticleCode = productRaw.ArticleCode;

            result.ProductTypeId = GetIdInDictionary<ProductType>(productRaw.ProductType, m_db.ProductTypes);

            result.ManufacturerId = GetIdInDictionary<Manufacturer>(productRaw.Manufacturer, m_db.Manufacturers);

            try
            {
                // If the price number is incorrect, we'll skip it.
                // Besides, there is a question which currency does the price come in.
                Decimal price = Decimal.Parse(productRaw.Price);
                result.Price = price;
            }
            catch (ArgumentNullException)
            { }
            catch (FormatException)
            { }
            catch (OverflowException)
            { }

            try
            {
                // Same for correctness of the format of this price.
                // Besides, in a real production system we probably would like to check if the discount price is less
                // than the original price, and display a validation error to the user in case it's not.
                Decimal discountPrice = Decimal.Parse(productRaw.DiscountPrice);
                result.DiscountPrice = discountPrice;
            }
            catch (ArgumentNullException)
            { }
            catch (FormatException)
            { }
            catch (OverflowException)
            { }

            result.DeliveryRangeId = GetIdInDictionary<DeliveryRange>(productRaw.DeliveryRange, m_db.DeliveryRanges);

            result.PersonTypeId = GetIdInDictionary<PersonType>(productRaw.PersonType, m_db.PersonTypes);

            result.SizeId = GetIdInDictionary<Size>(productRaw.Size, m_db.Sizes);

            result.ColorId = GetIdInDictionary<Color>(productRaw.Color, m_db.Colors);

            return result;
        }

        /// <summary>
        /// Gets the dictionary id by its name.
        /// If the value exists, returns its id.
        /// If the value doesn't exist, creates it in the database and then returns its id.
        /// </summary>
        /// <typeparam name="T">Type of value in a dictionary</typeparam>
        /// <param name="valueRaw">String name of the dictionary item</param>
        /// <param name="dbCollection">Collection of dictionary values</param>
        /// <returns>Id of the dictionary item</returns>
        private int? GetIdInDictionary<T>(String valueRaw, DbSet<T> dbCollection)
            where T : class, INamedDictionary, new()
        {
            if (String.IsNullOrEmpty(valueRaw))
                return null;

            // first try to get the dictionary value by name from cache

            // if this collection is not cached yet, let's cache it
            var collectionName = typeof(T).Name;
            if (!m_dictionaryValues.ContainsKey(collectionName))
            {
                m_dictionaryValues.Add(collectionName, dbCollection.ToDictionary(x => x.Name, x => x.Id));
            }

            // now check if there is value in cache
            var currentCollectionValues = m_dictionaryValues[collectionName];

            // if value is present, return the id
            if (currentCollectionValues.ContainsKey(valueRaw))
                return currentCollectionValues[valueRaw];

            // if value is not in cache, we need to create it in the database
            T valueToCreate = new T() { Name = valueRaw };
            var valueDb = dbCollection.Add(valueToCreate);
            m_db.SaveChanges();

            // and then cache it and return it
            if (valueDb != null)
            {
                currentCollectionValues.Add(valueRaw, valueDb.Id);
                return valueDb.Id;
            }

            return null;
        }
    }
}