using AssignmentWebshop.Models;
using AssignmentWebshop.ProductImport.ProductValidations;
using System;
using System.Threading;

namespace AssignmentWebshop.ProductImport
{
    /// <summary>
    /// Converts the product from a <see cref="ProductRaw"/> raw value, coming from user input,
    /// to a valid <see cref="Product"/> product object.
    /// </summary>
    public class ProductRawToProductConverter
    {
        static ReaderWriterLockSlim locker = new ReaderWriterLockSlim();
        
        IDictionaryCache m_dictionaryCache;
        IProductValidator m_productValidator;

        public ProductRawToProductConverter(IDictionaryCache dictionaryCache, IProductValidator validator)
        {
            m_dictionaryCache = dictionaryCache;
            m_productValidator = validator;
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

            var validationResults = m_productValidator.Validate(productRaw);
            if (validationResults != null && validationResults.Count > 0)
            {
                return null;
            }

            Product result = new Product();

            result.ProductName = productRaw.ProductName;

            result.ArticleCode = productRaw.ArticleCode;

            result.ProductTypeId = m_dictionaryCache.GetIdInDictionary<ProductType>(productRaw.ProductType);

            result.ManufacturerId = m_dictionaryCache.GetIdInDictionary<Manufacturer>(productRaw.Manufacturer);

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

            result.DeliveryRangeId = m_dictionaryCache.GetIdInDictionary<DeliveryRange>(productRaw.DeliveryRange);

            result.PersonTypeId = m_dictionaryCache.GetIdInDictionary<PersonType>(productRaw.PersonType);

            result.SizeId = m_dictionaryCache.GetIdInDictionary<Size>(productRaw.Size);

            result.ColorId = m_dictionaryCache.GetIdInDictionary<Color>(productRaw.Color);

            return result;
        }
    }
}