using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AssignmentWebshop.ProductImport.ProductValidations
{
    public class ValidationResult {
        public ValidationResult(String errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public String ErrorMessage { get; private set; }
    }
}