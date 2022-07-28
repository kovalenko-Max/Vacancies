using GraphQL;
using GraphQL.Types;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLEngine.Features.Vacancy.DeleteVacancy
{
    internal static class DeleteVacancyMutation
    {
        public static void AddDeleteVacancyMutation(this MutationType type)
        {
            type.FieldAsync<DeleteVacancyOutputGraphType>(
                "DeleteVacancy",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<StringGraphType>> { Name = "id", Description = "id of the vacancy for removing" }),
                    resolve: async context => await context.RequestServices.GetRequiredService<IVacancyStorage>().DeleteAsync(context.GetArgument<Guid>("id"))
                );
        }
    }
}
