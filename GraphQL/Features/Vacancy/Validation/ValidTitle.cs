using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.Validation
{
    internal class ValidTitle
    {
        private const int UpperLenghtLimit = 200;

        public readonly string Value;

        private ValidTitle(string value)
        {
            Value = value;
        }

        public static Result<ValidTitle, VacancyValidationException> Create(string value)
        {
            value = value.Trim();

            if (string.IsNullOrEmpty(value))
            {
                return new VacancyTitleValidationException($"Title cannot be empty");
            }

            if (value.Length > UpperLenghtLimit)
            {
                return new VacancyTitleValidationException($"Title cannot be longer than {UpperLenghtLimit} characters");
            }

            return new ValidTitle(value);
        }

        public static implicit operator string(ValidTitle validDescription) => validDescription.Value;
    }
}
