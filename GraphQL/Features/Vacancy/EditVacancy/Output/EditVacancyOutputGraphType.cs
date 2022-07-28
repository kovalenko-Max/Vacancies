using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Output
{
    internal class EditVacancyOutputGraphType : ObjectGraphType<EditVacancyOutput>
    {
        public EditVacancyOutputGraphType()
        {
            Name = nameof(EditVacancyOutputGraphType);

            Field<EditVacancyOutputDataGraphType>().Name("data")
                .Resolve(d => d.Source.Data);

            Field<ListGraphType<VacancyExceptionGraphType>>()
                .Name("errors")
                .Resolve(e => e.Source.Errors);
        }
    }
}
