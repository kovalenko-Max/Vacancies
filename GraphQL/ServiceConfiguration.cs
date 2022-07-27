using GraphQL;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using GraphQL.SystemTextJson;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GraphQLEngine
{
    public static class ServiceConfigurationExtentions
    {
        public static void AddGraphQLEngineServices(this IServiceCollection services)
        {
            services.AddGraphQL(b => b
                .AddApolloTracing()
                .AddHttpMiddleware<VacancySchema>()
                .AddSystemTextJson()
                .AddSchema<VacancySchema>()
                .AddGraphTypes(typeof(VacancySchema).Assembly));
        }

        public static void UseGraphQLEngine(this IApplicationBuilder app)
        {
            app.UseGraphQL<VacancySchema>();
            app.UseGraphQLPlayground();
            app.UseGraphQLGraphiQL();
            app.UseGraphQLVoyager();
        }
    }
}
