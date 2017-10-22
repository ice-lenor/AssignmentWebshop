using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    /// <summary>
    /// Interface of all product validations
    /// </summary>
    interface IValidation
    {
        ValidationResult Validate(ProductRaw product);
    }
}
