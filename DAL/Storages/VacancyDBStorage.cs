using App.Entities;
using GraphQLEngine.Features.Vacancy;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using GraphQLEngine.Features.Vacancy.GetVacancies.Output;
using Microsoft.EntityFrameworkCore;

namespace DAL.Storages
{
    public class VacancyDBStorage : IVacancyStorage
    {
        private readonly ApplicationContext _context;

        public VacancyDBStorage(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<CreateVacancyOutput> AddVacancy(CreateVacancyInput vacancy)
        {
            var entity = new VacancyEntity(vacancy.Title, vacancy.Description);

            var result = await _context.Vacancy.AddAsync(entity);

            await _context.SaveChangesAsync();

            return new CreateVacancyOutput(result.Entity.Id, result.Entity.Title, result.Entity.Description);
        }

        public async Task<IEnumerable<GetVacancyOutput>> GetAll()
        {
            var entities = await _context.Vacancy.AsNoTracking().ToListAsync();

            var result = new List<GetVacancyOutput>();

            entities.ForEach(e => result.Add(new GetVacancyOutput(e.Id, e.Title, e.Description)));

            return result;
        }

        public async Task<GetVacancyOutput?> GetById(Guid id)
        {
            var entity = _context.Vacancy.AsNoTracking().FirstOrDefault(e => e.Id == id);

            return entity != null ? new GetVacancyOutput(entity.Id, entity.Title, entity.Description) : null;
        }

        public async Task<DeleteVacancyOutput> Delete(Guid id)
        {
            var result = _context.Vacancy.Remove(await _context.Vacancy.Where(e => e.Id == id).FirstOrDefaultAsync());

            await _context.SaveChangesAsync();

            return new DeleteVacancyOutput(result.Entity.Id, result.Entity.Title, result.Entity.Description);
        }
    }
}
