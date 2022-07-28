using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.Validation;
internal class ValidDescription
{
    private const int LowerLenghtLimit = 30;

    public readonly string Value;

    private ValidDescription(string value)
    {
        Value = value;
    }

    public static Result<ValidDescription, VacancyValidationException> Create(string value)
    {
        value = value.Trim();

        if (string.IsNullOrEmpty(value))
        {
            return new VacancyDescriptionValidationExeption($"Description cannot be empty");
        }

        if (value.Length < LowerLenghtLimit)
        {
            return new VacancyDescriptionValidationExeption($"Description cannot be less than {LowerLenghtLimit} characters");
        }

        return new ValidDescription(value);
    }

    public static implicit operator string(ValidDescription validDescription) => validDescription.Value;
}
