namespace GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
public record CreateVacancyInput(string Title, string Description, long? WageFrom = default, long? WageTo = default);