using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Repositories.Entities;

namespace ConfigurationService.Entities.Repositories.Interfaces
{
    public interface IProgramsRepository
    {
        Task<Guid> Add();
        Task<IEnumerable<ProgramEntity>> GetAll();


    }
}
