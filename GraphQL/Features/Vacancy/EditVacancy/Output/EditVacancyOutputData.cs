namespace GraphQLEngine.Features.Vacancy.EditVacancy.Output;
public record EditVacancyOutputData(Guid Id, string Title, string Description, long? WageFrom = default, long? WageTo = default);