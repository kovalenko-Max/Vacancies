using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Output
{
    internal class EditVacancyOutputDataGraphType : ObjectGraphType<EditVacancyOutputData>
    {
        public EditVacancyOutputDataGraphType()
        {
            Name = nameof(EditVacancyOutputDataGraphType);

            Field(ve => ve.Id);
            Field(ve => ve.Title);
            Field(ve => ve.Description);
            Field(ve => ve.WageFrom, nullable: true);
            Field(ve => ve.WageTo, nullable: true);
        }
    }
}
