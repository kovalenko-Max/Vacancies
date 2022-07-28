using DAL;
using Dapper;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.SystemTextJson;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace VacancyAPI.Test.Config
{
    internal class CustomWebApplicationFactory : WebApplicationFactory<Program>
    {
        public string TestDatabaseConnectionString { get; private set; }

        public CustomWebApplicationFactory()
        {
            TestDatabaseConnectionString = "User ID=postgres;Password=admin;Host=localhost;Port=5432;Database=VacanciesDB.Test;";
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<ApplicationContext>));
                if (descriptor == null)
                {
                    throw new NullReferenceException("DB context service did not found.");
                }
                services.Remove(descriptor);

                services.AddDbContext<ApplicationContext>(builder =>
                {
                    builder.UseNpgsql(TestDatabaseConnectionString);
                });

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<ApplicationContext>();
                    var logger = scopedServices
                        .GetRequiredService<ILogger<CustomWebApplicationFactory>>();
                }
            });
        }

        public GraphQLHttpClient CreateGraphQLClient(HttpClient httpClient)
        {
            var graphqlHttpClient = new GraphQLHttpClient(
                options: new GraphQLHttpClientOptions
                {
                    EndPoint = new Uri($"http://localhost/graphql")
                },
                 serializer: new SystemTextJsonSerializer(options =>
                 {
                     options.Converters.RemoveAt(0);
                     options.Converters.Add(new JsonStringEnumConverter());
                     options.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                     options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                 }),
                httpClient);

            return graphqlHttpClient;
        }

        public async Task ReCreateDBSchemaAsync(DbContextOptions<ApplicationContext> options)
        {
            var context = new ApplicationContext(options);

            using var connection = context.Database.GetDbConnection();

            await connection.ExecuteAsync("drop schema if exists public cascade;");
            await connection.ExecuteAsync("create schema public;");

            await connection.CloseAsync();
        }

        public async Task DropTestDBAsync(DbContextOptions<ApplicationContext> options)
        {
            var context = new ApplicationContext(options);

            using var connection = context.Database.GetDbConnection();

            await connection.ExecuteAsync("drop schema if exists public cascade;");

            await connection.CloseAsync();
        }
    }
}
