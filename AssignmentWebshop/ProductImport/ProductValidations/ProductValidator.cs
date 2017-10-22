using System.Collections.Generic;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    /// <summary>
    /// Validates a product and returns everything that's wrong with it
    /// </summary>
    public class ProductValidator : IProductValidator
    {
        private List<IValidation> m_validations;

        public ProductValidator()
        {
            m_validations = new List<IValidation>
            {
                new ValidationArticleCode(),
                new ValidationProductName(),
                // here can go all other validations, if we have them
            };
        }

        public ICollection<ValidationResult> Validate(ProductRaw product)
        {
            var validationResults = new List<ValidationResult>();
            foreach(var validation in m_validations) {
                var result = validation.Validate(product);
                if (result != null)
                    validationResults.Add(result);
            }

            return validationResults;
        }
    }
}