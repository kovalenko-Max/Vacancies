using GetVacancies.Features.Vacancy.CreateVacancy;
using GraphQL.Types;

namespace GraphQLEngine
{
    internal class QueryType : ObjectGraphType
    {
        public QueryType()
        {
            Name = nameof(QueryType);

            this.AddVacancyQuery();
        }
    }
}
