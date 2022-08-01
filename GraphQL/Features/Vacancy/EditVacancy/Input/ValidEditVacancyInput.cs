﻿using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
using GraphQLEngine.Features.Vacancy.Validation;
using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Input;
public record ValidEditVacancyInput(Guid Id, string Title, string Description, long? WageFrom = default, long? WageTo = default)
{
    public static async Task<Result<ValidEditVacancyInput, VacancyValidationException>> From(EditVacancyInput input, IVacancyStorage storage)
    {
        var errors = new List<VacancyValidationException>();

        var titleValidationResult = ValidTitle.Create(input.Title);
        var descriptionValidationResult = ValidDescription.Create(input.Description);
        var wageValidationResult = WageValidator.Validate(input.WageFrom, input.WageTo);

        if (!await storage.IsVacancyIdExist(input.Id))
        {
            errors.Add(new VacancyIdValidationException($"Vacancy with id: {input.Id} does not exist"));
        }

        errors.AddRange(titleValidationResult.Errors);
        errors.AddRange(descriptionValidationResult.Errors);
        errors.AddRange(wageValidationResult.Errors);

        if (errors.Any())
        {
            return errors.ToArray();
        }

        if (wageValidationResult.GetValueForSure().IsExist)
        {
            return new ValidEditVacancyInput(
            input.Id,
            titleValidationResult.GetValueForSure(),
            descriptionValidationResult.GetValueForSure(),
            wageValidationResult.GetValueForSure().WageFrom,
            wageValidationResult.GetValueForSure().WageTo);
        }
        else
        {
            return new ValidEditVacancyInput(
                input.Id,
            titleValidationResult.GetValueForSure(),
            descriptionValidationResult.GetValueForSure());
        }
    }
}
