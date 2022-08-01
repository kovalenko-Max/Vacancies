using App.Entities;
using DAL;
using GraphQL.Client.Http;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
using Microsoft.EntityFrameworkCore;
using VacancyAPI.Test.Config;
using VacancyAPI.Test.Helpers;

namespace VacancyAPI.Test.GraphQLRequestTests.Features.Vacancy.EditVacancy;

internal record EditVacancyResponce(EditVacancyResponceModel EditVacancy);
internal record EditVacancyResponceModel(EditVacancyOutputData? Data, EditVacancyValidationErrorResponce[]? Errors);
internal record EditVacancyValidationErrorResponce(string Field, string Message);
internal class EditVacancyMutation
{
    private CustomWebApplicationFactory _factory;
    private DbContextOptions<ApplicationContext> _dbContextOptions;
    private ApplicationContext _context;
    private GraphQLHttpClient _graphQLHttp;

    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        _factory = new CustomWebApplicationFactory();
        var httpClient = _factory.CreateClient();
        _graphQLHttp = _factory.CreateGraphQLClient(httpClient);

        var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
        dbContextOptionsBuilder.UseNpgsql(_factory.TestDatabaseConnectionString);
        dbContextOptionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

        _dbContextOptions = dbContextOptionsBuilder.Options;
    }

    [SetUp]
    public async Task SetUp()
    {
        await _factory.ReCreateDBSchemaAsync(_dbContextOptions);
        _context = new ApplicationContext(_dbContextOptions);
    }

    [Test]
    public async Task EditVacancyMutation_ValidInput_VacancyWasEditedInDB()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(vacancyEntity.Id, "EditedVacansyTestTitle", "EditedVacancyTestDescription_edited_edited");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;

        Assert.Null(editVacancyOutput.Errors);
        Assert.NotNull(editVacancyOutput.Data);
        Assert.That(editVacancyOutput.Data.Title, Is.EqualTo(input.Title));
        Assert.That(editVacancyOutput.Data.Description, Is.EqualTo(input.Description));
        Assert.That(editVacancyOutput.Data.WageFrom, Is.EqualTo(input.WageFrom));
        Assert.That(editVacancyOutput.Data.WageTo, Is.EqualTo(input.WageTo));

        var dbEntry = await _context.Vacancy.Where(v => v.Id == editVacancyOutput.Data.Id).FirstOrDefaultAsync();

        Assert.NotNull(dbEntry);
        Assert.That(editVacancyOutput.Data.Id, Is.EqualTo(dbEntry.Id));
        Assert.That(editVacancyOutput.Data.Title, Is.EqualTo(dbEntry.Title));
        Assert.That(editVacancyOutput.Data.Description, Is.EqualTo(dbEntry.Description));
        Assert.That(editVacancyOutput.Data.WageFrom, Is.EqualTo(dbEntry.WageFrom));
        Assert.That(editVacancyOutput.Data.WageTo, Is.EqualTo(dbEntry.WageTo));
    }

    [Test]
    public async Task EditVacancyMutation_IdDoesNotExist_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(Guid.NewGuid(), "EditedVacansyTestTitle", "EditedVacancyTestDescription_edited_edited");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo($"Vacancy with id: {input.Id} does not exist"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Id"));
    }

    [Test]
    public async Task EditVacancyMutation_EmptyTitle_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(vacancyEntity.Id,
            "",
            "EditedVacancyTestDescription_edited_edited");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Title cannot be empty"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Title"));
    }

    [Test]
    public async Task EditVacancyMutation_NotValidTitle_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(vacancyEntity.Id,
            "NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle" +
            "_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle",
            "EditedVacancyTestDescription_edited_edited");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Title cannot be longer than 200 characters"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Title"));
    }

    [Test]
    public async Task EditVacancyMutation_EmptyDescription_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(vacancyEntity.Id,
            "EditedVacansyTestTitle",
            "");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Description cannot be empty"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Description"));
    }

    [Test]
    public async Task EditVacancyMutation_NotValidDescription_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(vacancyEntity.Id,
            "EditedVacansyTestTitle",
            "NotValidDescriprion");

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Description cannot be less than 30 characters"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Description"));
    }

    [Test]
    public async Task EditVacancyMutation_NotValidInput_ReturnValidationError()
    {
        var vacancyEntity = new VacancyEntity("InitialTitle", "InitialDescription_description_description");
        _context.Vacancy.Add(vacancyEntity);
        await _context.SaveChangesAsync();

        var input = new EditVacancyInput(Guid.NewGuid(),
            "NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle" +
            "_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle",
            "NotValidDescriprion",
            1000,
            500);

        var request = TestDataSource.GetEditVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<EditVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.EditVacancy);

        var editVacancyOutput = result.Data.EditVacancy;
        Assert.NotNull(editVacancyOutput.Errors);
        Assert.IsTrue(editVacancyOutput.Errors.Count() == 4);

        var titleValidationError = editVacancyOutput.Errors;

        Assert.NotNull(titleValidationError[0]);
        Assert.That(titleValidationError[0].Message,
            Is.EqualTo($"Vacancy with id: {input.Id} does not exist"));
        Assert.That(titleValidationError[0].Field,
            Is.EqualTo("Id"));

        Assert.NotNull(titleValidationError[1]);
        Assert.That(titleValidationError[1].Message,
            Is.EqualTo("Title cannot be longer than 200 characters"));
        Assert.That(titleValidationError[1].Field,
            Is.EqualTo("Title"));

        Assert.NotNull(titleValidationError[2]);
        Assert.That(titleValidationError[2].Message,
            Is.EqualTo("Description cannot be less than 30 characters"));
        Assert.That(titleValidationError[2].Field,
            Is.EqualTo("Description"));

        Assert.NotNull(titleValidationError[3]);
        Assert.That(titleValidationError[3].Message,
            Is.EqualTo("WageFrom cannot be bigger than wageTo"));
        Assert.That(titleValidationError[3].Field,
            Is.EqualTo("wageTo"));
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.ReCreateDBSchemaAsync(_dbContextOptions);
    }
}