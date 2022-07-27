using DAL.Storages;
using GraphQLEngine.Features.Vacancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DAL
{
    public static class ServiceConfigurationExtentions
    {
        public static void AddDALServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationContext>(builder =>
            {
                builder.UseNpgsql(connectionString);
            });

            services.AddScoped<IVacancyStorage, VacancyDBStorage>();
        }
    }
}