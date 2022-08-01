using App.Entities;
using DAL;
using GraphQL.Client.Http;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;
using Microsoft.EntityFrameworkCore;
using VacancyAPI.Test.Config;
using VacancyAPI.Test.GraphQLRequestTests.Features.Vacancy.EditVacancy;
using VacancyAPI.Test.Helpers;

namespace VacancyAPI.Test.GraphQLRequestTests.Features.Vacancy.CreateVacancy;
internal record CreateVacancyResponce(CreateVacancyResponceModel CreateVacancy);
internal record CreateVacancyResponceModel(CreateVacancyOutputData? Data, CreateVacancyValidationErrorResponce[]? Errors);
internal record CreateVacancyValidationErrorResponce(string Field, string Message);
internal class CreateVacancyMutation
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
    public async Task CreateVacancyMutation_VacancyWasAddedToDB()
    {
        var input = new CreateVacancyInput(
            "CreateVacansyTestTitle",
            "ValidCreateVacancyTestDescription",
            1000,
            2000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var createVacancyOutput = result.Data.CreateVacancy.Data;
        Assert.NotNull(createVacancyOutput);
        Assert.That(createVacancyOutput.Title, Is.EqualTo(input.Title));
        Assert.That(createVacancyOutput.Description, Is.EqualTo(input.Description));
        Assert.That(createVacancyOutput.WageFrom, Is.EqualTo(input.WageFrom));
        Assert.That(createVacancyOutput.WageTo, Is.EqualTo(input.WageTo));

        var dbEntry = await _context.Vacancy.Where(v => v.Id == createVacancyOutput.Id).FirstOrDefaultAsync();

        Assert.NotNull(dbEntry);
        Assert.That(createVacancyOutput.Id, Is.EqualTo(dbEntry.Id));
        Assert.That(createVacancyOutput.Title, Is.EqualTo(dbEntry.Title));
        Assert.That(createVacancyOutput.Description, Is.EqualTo(dbEntry.Description));
        Assert.That(createVacancyOutput.WageFrom, Is.EqualTo(dbEntry.WageFrom));
        Assert.That(createVacancyOutput.WageTo, Is.EqualTo(dbEntry.WageTo));
    }

    [Test]
    public async Task CreateVacancyMutation_EmptyTitle_ReturnValidationError()
    {
        var input = new CreateVacancyInput(
            string.Empty,
            "EditedVacancyTestDescription_edited_edited",
            1000,
            2000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var editVacancyOutput = result.Data.CreateVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Title cannot be empty"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Title"));
    }

    [Test]
    public async Task CreateVacancyMutation_NotValidTitle_ReturnValidationError()
    {
        var input = new CreateVacancyInput(
            "NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle" +
            "_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle",
            "CreatedVacancyTestDescription_edited_edited",
            1000,
            2000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var editVacancyOutput = result.Data.CreateVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Title cannot be longer than 200 characters"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Title"));
    }

    [Test]
    public async Task CreateVacancyMutation_EmptyDescription_ReturnValidationError()
    {
        var input = new CreateVacancyInput("CreatedVacansyTestTitle", string.Empty, 1000, 2000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var editVacancyOutput = result.Data.CreateVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Description cannot be empty"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Description"));
    }

    [Test]
    public async Task CreateVacancyMutation_NotValidDescription_ReturnValidationError()
    {
        var input = new CreateVacancyInput(
            "EditedVacansyTestTitle",
            "NotValidDescriprion",
            1000,
            2000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var editVacancyOutput = result.Data.CreateVacancy;
        Assert.NotNull(editVacancyOutput.Errors);

        var titleValidationError = editVacancyOutput.Errors.FirstOrDefault();
        Assert.NotNull(titleValidationError);
        Assert.That(titleValidationError.Message,
            Is.EqualTo("Description cannot be less than 30 characters"));
        Assert.That(titleValidationError.Field,
            Is.EqualTo("Description"));
    }

    [Test]
    public async Task CreateVacancyMutation_NotValidInput_ReturnValidationError()
    {
        var input = new CreateVacancyInput(
            "NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle" +
            "_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle_NotValidTitle",
            "NotValidDescriprion",
            2000,
            1000);

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var editVacancyOutput = result.Data.CreateVacancy;
        Assert.NotNull(editVacancyOutput.Errors);
        Assert.IsTrue(editVacancyOutput.Errors.Count() == 3);

        var titleValidationError = editVacancyOutput.Errors;

        Assert.NotNull(titleValidationError[0]);
        Assert.That(titleValidationError[0].Message,
            Is.EqualTo("Title cannot be longer than 200 characters"));
        Assert.That(titleValidationError[0].Field,
            Is.EqualTo("Title"));

        Assert.NotNull(titleValidationError[1]);
        Assert.That(titleValidationError[1].Message,
            Is.EqualTo("Description cannot be less than 30 characters"));
        Assert.That(titleValidationError[1].Field,
            Is.EqualTo("Description"));

        Assert.NotNull(titleValidationError[2]);
        Assert.That(titleValidationError[2].Message,
            Is.EqualTo("WageFrom cannot be bigger than wageTo"));
        Assert.That(titleValidationError[2].Field,
            Is.EqualTo("wageTo"));
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.ReCreateDBSchemaAsync(_dbContextOptions);
    }
}