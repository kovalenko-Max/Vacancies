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
            Field(V => V.Description).Description("Vacancy description");
            Field(V => V.WageFrom, nullable: true);
            Field(V => V.WageTo, nullable: true);
        }
    }
}
