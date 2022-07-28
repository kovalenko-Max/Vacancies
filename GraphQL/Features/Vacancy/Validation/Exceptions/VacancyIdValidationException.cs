namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions;
internal class VacancyIdValidationException : VacancyValidationException
{
    private const string FieldName = "Id";
    public VacancyIdValidationException(string message) : base(FieldName, message)
    {
    }
}