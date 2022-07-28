using DAL;
using GraphQL.Client.Http;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using Microsoft.EntityFrameworkCore;
using VacancyAPI.Test.Config;
using VacancyAPI.Test.Helpers;

namespace VacancyAPI.Test.GraphQLRequestTests.Features.Vacancy.CreateVacancy;
internal record CreateVacancyResponce(CreateVacancyOutput CreateVacancy);
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
        var input = new CreateVacancyInput("CreateVacansyTestTitle", "CreateVacancyTestDescription");

        var request = TestDataSource.GetCreateVacancyGraphQLRequest(input);

        var result = await _graphQLHttp.SendMutationAsync<CreateVacancyResponce>(request);

        Assert.NotNull(result.Data);
        Assert.NotNull(result.Data.CreateVacancy);

        var createVacancyOutput = result.Data.CreateVacancy;

        Assert.That(createVacancyOutput.Title, Is.EqualTo(input.Title));
        Assert.That(createVacancyOutput.Description, Is.EqualTo(input.Description));

        var dbEntry = await _context.Vacancy.Where(v=> v.Id == createVacancyOutput.Id).FirstOrDefaultAsync();

        Assert.NotNull(dbEntry);
        Assert.That(createVacancyOutput.Id, Is.EqualTo(dbEntry.Id));
        Assert.That(createVacancyOutput.Title, Is.EqualTo(dbEntry.Title));
        Assert.That(createVacancyOutput.Description, Is.EqualTo(dbEntry.Description));
    }

    [OneTimeTearDown]
    public async Task OneTimeTearDown()
    {
        await _factory.ReCreateDBSchemaAsync(_dbContextOptions);
    }
}