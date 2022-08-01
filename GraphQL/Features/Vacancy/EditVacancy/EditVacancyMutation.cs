using GraphQL.Types;
using GraphQL;
using Microsoft.Extensions.DependencyInjection;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;

namespace GraphQLEngine.Features.Vacancy.EditVacancy;
internal static class EditVacancyMutation
{
    public static void AddEditVacancyMutation(this MutationType type)
    {
        type.FieldAsync<EditVacancyOutputGraphType>(
            "EditVacancy",
            arguments: new QueryArguments(
                new QueryArgument<NonNullGraphType<EditVacancyInputGraphType>> { Name = "vacancy" }
            ),
        resolve: async context =>
        {
            ArgumentNullException.ThrowIfNull(context.RequestServices);
            var vacancy = context.GetArgument<EditVacancyInput>("vacancy");

            var storage = context.RequestServices
                .GetRequiredService<IVacancyStorage>();

            var validationResult = await ValidEditVacancyInput.From(vacancy, storage);

            if (validationResult.IsFailed)
            {
                return new EditVacancyOutput(Errors: validationResult.Errors);
            }

            return new EditVacancyOutput(
                Data: await storage
                .EditVacancyAsync(validationResult.GetValueForSure()));
        });
    }
}
