using GraphQLEngine.Features.Vacancy.Validation.Exceptions;
using GraphQLEngine.Features.Vacancy.Validation;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
public record ValidCreateVacancyInput(string Title, string Description, long? WageFrom = default, long? WageTo = default)
{
    public static Result<ValidCreateVacancyInput, VacancyValidationException> From(CreateVacancyInput input)
    {
        var errors = new List<VacancyValidationException>();

        var titleValidationResult = ValidTitle.Create(input.Title);
        var descriptionValidationResult = ValidDescription.Create(input.Description);
        var wageValidationResult = WageValidator.Validate(input.WageFrom, input.WageTo);

        errors.AddRange(titleValidationResult.Errors);
        errors.AddRange(descriptionValidationResult.Errors);
        errors.AddRange(wageValidationResult.Errors);

        if (errors.Any())
        {
            return errors.ToArray();
        }

        if (wageValidationResult.GetValueForSure().IsExist)
        {
            return new ValidCreateVacancyInput(
            titleValidationResult.GetValueForSure(),
            descriptionValidationResult.GetValueForSure(),
            wageValidationResult.GetValueForSure().WageFrom,
            wageValidationResult.GetValueForSure().WageTo);
        }
        else
        {
            return new ValidCreateVacancyInput(
                titleValidationResult.GetValueForSure(),
                descriptionValidationResult.GetValueForSure());
        }
    }
}
