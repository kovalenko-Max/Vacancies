using GraphQLEngine.Features.Vacancy;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Input;
using GraphQLEngine.Features.Vacancy.CreateVacancy.Output;
using GraphQLEngine.Features.Vacancy.DeleteVacancy.Output;
using GraphQLEngine.Features.Vacancy.GetVacancies.Output;

namespace DAL.Storages
{
    public class VacancyInMemoryStorage : IVacancyStorage
    {
        private List<Vacancy> _vacancies;

        public VacancyInMemoryStorage()
        {
            _vacancies = new List<Vacancy>();
        }

        public async Task<CreateVacancyOutput> AddVacancy(CreateVacancyInput createVacancy)
        {
            var vacancy = new Vacancy(createVacancy.Title, createVacancy.Description);

            _vacancies.Add(vacancy);

            return new CreateVacancyOutput(vacancy.Id, vacancy.Title, vacancy.Description);
        }

        public Task<DeleteVacancyOutput> Delete(Guid guid)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetVacancyOutput>> GetAll()
        {
            List<GetVacancyOutput> getVacancyOutput = new List<GetVacancyOutput>();

            foreach(var vacancy in _vacancies)
            {
                getVacancyOutput.Add(new GetVacancyOutput(vacancy.Id, vacancy.Title, vacancy.Description));
            }

            return getVacancyOutput;
        }

        public async Task<GetVacancyOutput?> GetById(Guid id)
        {
            var vacancy = _vacancies.Where(v => v.Id == id).FirstOrDefault();

            return vacancy != null ? new GetVacancyOutput(vacancy.Id, vacancy.Title, vacancy.Description) : null;
        }
    }

    public class Vacancy
    {
        public Guid Id { get; }
        public string Title { get; }
        public string? Description { get; }

        public Vacancy(Guid guid, string title, string? description)
        {
            Id = guid;
            Title = title;
            Description = description;
        }

        public Vacancy(Guid guid, string title)
        {
            Id = guid;
            Title = title;
        }

        public Vacancy(string title, string? description)
        {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
        }

        public Vacancy(string title)
        {
            Id = Guid.NewGuid();
            Title = title;
        }
    }
}
