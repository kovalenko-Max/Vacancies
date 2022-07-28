using App.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class ApplicationContext : DbContext
    {
        public DbSet<VacancyEntity> Vacancy { get; private set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            ArgumentNullException.ThrowIfNull(Vacancy);

            if (Database.IsRelational())
            {
                Database.Migrate();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VacancyEntity>();
        }
    }
}