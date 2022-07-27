using GetVacancies.Features.Vacancy.CreateVacancy;
using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.CreateVacancy;

namespace GraphQLEngine
{
    public class VacancySchema : Schema
    {
        public VacancySchema(IServiceProvider provider) : base(provider)
        {
            Query = (QueryType)provider.GetService(typeof(QueryType)) ?? throw new InvalidOperationException();
            Mutation = (MutationType)provider.GetService(typeof(MutationType)) ?? throw new InvalidOperationException();
        }
    }
}
