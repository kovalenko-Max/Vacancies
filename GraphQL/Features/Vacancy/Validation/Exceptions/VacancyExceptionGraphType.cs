using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.Validation.Exceptions
{
    internal class VacancyExceptionGraphType : ObjectGraphType<VacancyValidationException>
    {
        public VacancyExceptionGraphType()
        {
            Name = nameof(VacancyExceptionGraphType);

            Field(ve => ve.Field);
            Field(ve => ve.Message);
        }
    }
}