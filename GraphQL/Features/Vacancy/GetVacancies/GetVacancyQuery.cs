using GraphQL;
using GraphQL.Types;
using GraphQLEngine;
using GraphQLEngine.Features.Vacancy;
using GraphQLEngine.Features.Vacancy.GetVacancies.Output;
using Microsoft.Extensions.DependencyInjection;

namespace GetVacancies.Features.Vacancy.CreateVacancy
{
    internal static class GetVacancyQuery
    {
        public static void AddVacancyQuery(this QueryType type)
        {
            type.FieldAsync<ListGraphType<GetVacancyOutputGraphType>>(
                "GetAllVacancies",
                resolve: async context => await context.RequestServices.GetRequiredService<IVacancyStorage>().GetAllAsync());

            type.FieldAsync<GetVacancyOutputGraphType>(
                "GetVacancyById",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the vacancy" }
                ),
                resolve: async context =>
                await context.RequestServices.GetRequiredService<IVacancyStorage>().GetByIdAsync(context.GetArgument<Guid>("id")));
        }
    }
}
