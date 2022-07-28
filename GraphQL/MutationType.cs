using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.CreateVacancy;
using GraphQLEngine.Features.Vacancy.DeleteVacancy;
using GraphQLEngine.Features.Vacancy.EditVacancy;

namespace GraphQLEngine
{
    internal class MutationType : ObjectGraphType
    {
        public MutationType()
        {
            Name = nameof(MutationType);

            this.AddDeleteVacancyMutation();
            this.AddCreateVacancyMutation();
            this.AddEditVacancyMutation();
        }
    }
}
