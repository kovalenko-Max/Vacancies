using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Output
{
    public class CreateVacancyOutputGraphType : ObjectGraphType<CreateVacancyOutput>
    {
        public CreateVacancyOutputGraphType()
        {
            Name = nameof(CreateVacancyOutputGraphType);

            Field(v => v.Id).Description("Vacancy Id");
            Field(V => V.Title).Description("Vacancy title");
            Field(V => V.Description, nullable: true).Description("Vacancy description");
        }
    }
}
