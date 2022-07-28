using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using GraphQLEngine.Features.Vacancy.EditVacancy.Input;
using GraphQLEngine.Features.Vacancy.EditVacancy.Output;
using GraphQLEngine.Features.Vacancy.GetVacancies.Output;

namespace GraphQLEngine.Features.Vacancy
{
    public interface IVacancyStorage
    {
        public Task<CreateVacancyOutput> AddVacancyAsync(CreateVacancyInput vacancy);
        public Task<DeleteVacancyOutput> DeleteAsync(Guid guid);
        public Task<EditVacancyOutputData> EditVacancyAsync(ValidEditVacancyInput vacancy);
        public Task<IEnumerable<GetVacancyOutput>> GetAllAsync();
        public Task<GetVacancyOutput?> GetByIdAsync(Guid guid);
        public Task<bool> IsVacancyIdExist(Guid id);
    }
}
