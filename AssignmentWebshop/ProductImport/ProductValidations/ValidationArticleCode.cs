using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    public class ValidationArticleCode : IValidation
    {
        public ValidationResult Validate(ProductRaw product)
        {
            if (String.IsNullOrEmpty(product.ArticleCode))
            {
                return new ValidationResult("Article code must not be empty");
            }
            if (product.ArticleCode.Length >= 150)
            {
                return new ValidationResult("Article code is too long");
            }

            return null;
        }
    }
}