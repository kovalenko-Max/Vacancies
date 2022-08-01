namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Output;

public record CreateVacancyOutputData(Guid Id, string Title, string Description, long? WageFrom = default, long? WageTo = default);
