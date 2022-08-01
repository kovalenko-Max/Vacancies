using GraphQL.Types;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Input
{
    internal class EditVacancyInputGraphType : InputObjectGraphType<EditVacancyInput>
    {
        public EditVacancyInputGraphType()
        {
            Name = nameof(EditVacancyInputGraphType);

            Field(v => v.Id);
            Field(v => v.Title);
            Field(v => v.Description);
            Field(v => v.WageFrom, nullable: true);
            Field(v => v.WageTo, nullable: true);
        }
    }
}
