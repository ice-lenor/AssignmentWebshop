using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    /// <summary>
    /// Validates a product and returns everything that's wrong with it
    /// </summary>
    public interface IProductValidator
    {
        ICollection<ValidationResult> Validate(ProductRaw product);
    }
}
