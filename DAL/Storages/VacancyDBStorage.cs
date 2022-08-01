using App.Entities;
using GraphQLEngine.Features.Vacancy;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
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

        public async Task<CreateVacancyOutputData> CreateVacancyAsync(ValidCreateVacancyInput vacancy)
        {
            var entity = new VacancyEntity(vacancy.Title, vacancy.Description, vacancy.WageFrom, vacancy.WageTo);

            var result = await _context.Vacancy.AddAsync(entity);

            await _context.SaveChangesAsync();

            return new CreateVacancyOutputData(
                result.Entity.Id, 
                result.Entity.Title, 
                result.Entity.Description, 
                result.Entity.WageFrom,
                result.Entity.WageTo);
        }

        public async Task<IEnumerable<GetVacancyOutput>> GetAllAsync()
        {
            var entities = await _context.Vacancy.AsNoTracking().ToListAsync();

            var result = new List<GetVacancyOutput>();

            entities.ForEach(e => result.Add(new GetVacancyOutput(e.Id, e.Title, e.Description, e.WageFrom, e.WageTo)));

            return result;
        }

        public async Task<GetVacancyOutput?> GetByIdAsync(Guid id)
        {
            var entity = await _context.Vacancy.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);

            return entity != null ? new GetVacancyOutput(entity.Id, entity.Title, entity.Description, entity.WageFrom, entity.WageTo) : null;
        }

        public async Task<DeleteVacancyOutput> DeleteAsync(Guid id)
        {
            var result = _context.Vacancy.Remove(await _context.Vacancy.Where(e => e.Id == id).FirstOrDefaultAsync());

            await _context.SaveChangesAsync();

            return new DeleteVacancyOutput(result.Entity.Id, result.Entity.Title, result.Entity.Description);
        }

        public async Task<EditVacancyOutputData> EditVacancyAsync(ValidEditVacancyInput vacancy)
        {
            var relatedEntity = await _context.Vacancy.FindAsync(vacancy.Id);

            ArgumentNullException.ThrowIfNull(relatedEntity);

            _context.Entry(relatedEntity).CurrentValues.SetValues(vacancy);

            await _context.SaveChangesAsync();

            return new EditVacancyOutputData(
                relatedEntity.Id, 
                relatedEntity.Title, 
                relatedEntity.Description, 
                relatedEntity.WageFrom, 
                relatedEntity.WageTo);
        }

        public async Task<bool> IsVacancyIdExist(Guid id)
        {
            return await _context.Vacancy.Where(v=>v.Id == id).AnyAsync();
        }
    }
}
