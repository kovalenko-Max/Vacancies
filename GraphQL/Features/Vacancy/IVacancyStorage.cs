using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using GraphQLEngine.Features.Vacancy.GetVacancies.Output;

namespace GraphQLEngine.Features.Vacancy
{
    public interface IVacancyStorage
    {
        public Task<CreateVacancyOutput> AddVacancy(CreateVacancyInput vacancy);
        public Task<DeleteVacancyOutput> Delete(Guid guid);
        public Task<IEnumerable<GetVacancyOutput>> GetAll();
        public Task<GetVacancyOutput?> GetById(Guid guid);
    }
}
