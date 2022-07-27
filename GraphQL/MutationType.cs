using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.CreateVacancy;
using GraphQLEngine.Features.Vacancy.DeleteVacancy;

namespace GraphQLEngine
{
    internal class MutationType : ObjectGraphType
    {
        public MutationType()
        {
            Name = nameof(MutationType);

            this.AddDeleteVacancyMutation();
            this.AddCreateVacancyMutation();
        }
    }
}
