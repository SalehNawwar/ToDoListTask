using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Validators
{
    public static class Validator
    {
        public static void Validate<T>(AbstractValidator<T> abstractValidator,T dto)
        {
            var result = abstractValidator.Validate(dto);
            if (result.IsValid == false)
            {
                throw new InvalidOperationException(result.Errors.First().ErrorMessage);
            }
        }
    }
}
