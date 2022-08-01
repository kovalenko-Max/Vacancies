using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions
{
    internal class VacancyWageValidationException : VacancyValidationException
    {
        public VacancyWageValidationException(string field, string message) : base(field, message)
        {
        }
    }
}
