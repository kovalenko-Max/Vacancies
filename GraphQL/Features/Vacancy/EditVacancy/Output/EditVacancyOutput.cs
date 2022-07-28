using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.EditVacancy.Output;
public record EditVacancyOutput(EditVacancyOutputData? Data = null, IEnumerable<VacancyValidationException>? Errors = null);