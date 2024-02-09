using ConfigurationService.Entities.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Repositories.Interfaces
{
    public interface IProvidersRepository
    {
        Task<ProviderConfigEntity> GetProviderConfig(Guid id);
        Task<IEnumerable<ProviderConfigEntity>> GetProviders();
        Task<Guid> Add(ProviderConfigEntity entity);
        Task<IEnumerable<ProviderConfigEntity>> GetByProgramId(Guid id);
    }
}
