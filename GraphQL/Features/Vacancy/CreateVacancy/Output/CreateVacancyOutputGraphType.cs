using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Output
{
    internal class CreateVacancyOutputGraphType : ObjectGraphType<CreateVacancyOutput>
    {
        public CreateVacancyOutputGraphType()
        {
            Name = nameof(CreateVacancyOutputGraphType);

            Field<CreateVacancyOutputDataGraphType>().Name("data")
               .Resolve(d => d.Source.Data);

            Field<ListGraphType<VacancyExceptionGraphType>>()
                .Name("errors")
                .Resolve(e => e.Source.Errors);
        }
    }
}
