using GraphQL.Client.Http;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;

namespace VacancyAPI.Test.Helpers
{
    internal static class TestDataSource
    {
        public static GraphQLHttpRequest GetCreateVacancyGraphQLRequest(CreateVacancyInput input)
        {
            return new GraphQLHttpRequest()
            {
                Query = @"
                    mutation($input: CreateVacancyInputGraphType!)
                    {
                      createVacancy(vacancy: $input)
                      {
                        id
                        title
                        description
                      }
                    }
                ",
                Variables = new
                {
                    input = input
                }
            };
        }
    }
}
