using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.GetVacancies.Output
{
    public class GetVacancyOutputGraphType : ObjectGraphType<GetVacancyOutput>
    {
        public GetVacancyOutputGraphType()
        {
            Name = nameof(GetVacancyOutputGraphType);

            Field(v => v.Id).Description("Vacancy Id");
            Field(V => V.Title).Description("Vacancy title");
            Field(V => V.Description, nullable: true).Description("Vacancy description");
        }
    }
}
