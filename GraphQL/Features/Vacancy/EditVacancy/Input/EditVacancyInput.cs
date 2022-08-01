namespace GraphQLEngine.Features.Vacancy.EditVacancy.Input;
public record EditVacancyInput(Guid Id, string Title, string Description, long? WageFrom = default, long? WageTo = default);