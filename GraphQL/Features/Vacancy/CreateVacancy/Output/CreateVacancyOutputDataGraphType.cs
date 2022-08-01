using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Output
{
    internal class CreateVacancyOutputDataGraphType : ObjectGraphType<CreateVacancyOutputData>
    {
        public CreateVacancyOutputDataGraphType()
        {
            Name = nameof(CreateVacancyOutputDataGraphType);

            Field(ve => ve.Id);
            Field(ve => ve.Title);
            Field(ve => ve.Description);
            Field(ve => ve.WageFrom, nullable: true);
            Field(ve => ve.WageTo, nullable: true);
        }
    }
}
