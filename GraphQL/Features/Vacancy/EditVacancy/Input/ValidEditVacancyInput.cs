using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
using GraphQLEngine.Features.Vacancy.Validation;
using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Input;
public record ValidEditVacancyInput(Guid Id, string Title, string? Description)
{
    public static async Task<Result<ValidEditVacancyInput, VacancyValidationException>> From(EditVacancyInput input, IVacancyStorage storage)
    {
        var errors = new List<VacancyValidationException>();

        var titleValidationResult = ValidTitle.Create(input.Title);
        var descriptionValidationResult = ValidDescription.Create(input.Description);

        if (!await storage.IsVacancyIdExist(input.Id))
        {
            errors.Add(new VacancyIdValidationException($"Vacancy with id: {input.Id} does not exist"));
        }

        errors.AddRange(titleValidationResult.Errors);
        errors.AddRange(descriptionValidationResult.Errors);

        if (errors.Any())
        {
            return errors.ToArray();
        }

        return new ValidEditVacancyInput(
            input.Id,
            titleValidationResult.GetValueForSure(),
            descriptionValidationResult.GetValueForSure());
    }
}
