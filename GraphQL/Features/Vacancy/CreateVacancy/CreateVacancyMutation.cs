using GraphQL;
using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy
{
    internal static class CreateVacancyMutation
    {
        public static void AddCreateVacancyMutation(this MutationType type)
        {
            type.FieldAsync<CreateVacancyOutputGraphType>(
                "CreateVacancy",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CreateVacancyInputGraphType>> { Name = "vacancy" }
                ),
            resolve: async context =>
            {
                var vacancy = context.GetArgument<CreateVacancyInput>("vacancy");
                return await context.RequestServices.GetRequiredService<IVacancyStorage>().AddVacancyAsync(vacancy);
            });
        }
    }
}
