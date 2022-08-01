using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
public record CreateVacancyOutput(CreateVacancyOutputData? Data = null, IEnumerable<VacancyValidationException>? Errors = null);