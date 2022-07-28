namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions
{
    internal class VacancyTitleValidationException : VacancyValidationException
    {
        private const string FieldName = "Title";
        public VacancyTitleValidationException(string message) : base(FieldName, message)
        {
        }
    }
}
