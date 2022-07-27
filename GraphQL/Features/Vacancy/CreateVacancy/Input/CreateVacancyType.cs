using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Input
{
    public class CreateVacancyInputGraphType : InputObjectGraphType<CreateVacancyInput>
    {
        public CreateVacancyInputGraphType()
        {
            Name = nameof(CreateVacancyInputGraphType);

            Field(v => v.Title);
            Field(v => v.Description, nullable: true);
        }
    }
}
