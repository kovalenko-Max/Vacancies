namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions
{
    public class VacancyValidationException : Exception
    {
        public string Field { get; init; }

        public VacancyValidationException(string field, string message) : base(message)
        {
            Field = field;
        }
    }
}
