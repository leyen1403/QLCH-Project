using QLCH.BLL.DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCH.BLL.Helpers
{
    public static class ValidationHelper
    {        
        public static void Validate<T>(T dto)
        {
            var context = new ValidationContext(dto, null, null);
            var results = new List<ValidationResult>();

            bool isValid = System.ComponentModel.DataAnnotations.Validator.TryValidateObject(dto, context, results, true);

            if (!isValid)
            {
                var errors = results.Select(e => e.ErrorMessage);
                throw new ValidationException(string.Join("\n", errors));
            }
        }

    }
}
