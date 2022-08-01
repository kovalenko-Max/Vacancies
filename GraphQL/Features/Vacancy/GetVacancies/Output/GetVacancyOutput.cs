namespace GraphQLEngine.Features.Vacancy.GetVacancies.Output;

public record GetVacancyOutput(Guid Id, string Title, string Description, long? WageFrom, long? WageTo);
