namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions
{
    internal class VacancyDescriptionValidationExeption : VacancyValidationException
    {
        private const string FieldName = "Description";
        public VacancyDescriptionValidationExeption(string message) : base(FieldName, message)
        {
        }
    }
}
