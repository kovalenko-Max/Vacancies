using GraphQL.Client.Http;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;

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

        public static GraphQLHttpRequest GetEditVacancyGraphQLRequest(EditVacancyInput input)
        {
            return new GraphQLHttpRequest()
            {
                Query = @"
                    mutation($input: EditVacancyInputGraphType!)
                    {
                      editVacancy(vacancy: $input)
                      {
                        data{
                          id
                          title
                          description
                        }
                        errors
                        {
                          field
                          message
                        }
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
