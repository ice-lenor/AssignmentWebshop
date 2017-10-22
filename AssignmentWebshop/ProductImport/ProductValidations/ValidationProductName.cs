using System;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    public class ValidationProductName : IValidation
    {
        public ValidationResult Validate(ProductRaw product)
        {
            if (String.IsNullOrEmpty(product.ProductName))
            {
                return new ValidationResult("ProductName must not be empty");
            }

            if (product.ProductName.Length >= 150)
            {
                return new ValidationResult("Article code is too long");
            }

            return null;
        }
    }
}