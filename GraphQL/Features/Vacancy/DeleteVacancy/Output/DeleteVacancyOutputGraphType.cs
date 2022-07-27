using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.DeleteVacancy.Output
{
    internal class DeleteVacancyOutputGraphType : ObjectGraphType<DeleteVacancyOutput>
    {
        public DeleteVacancyOutputGraphType()
        {
            Name = nameof(DeleteVacancyOutputGraphType);

            Field(v => v.Id).Description("Vacancy Id");
            Field(V => V.Title).Description("Vacancy title");
            Field(V => V.Description, nullable: true).Description("Vacancy description");
        }
    }
}
