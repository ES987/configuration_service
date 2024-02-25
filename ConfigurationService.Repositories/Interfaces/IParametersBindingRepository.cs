using ConfigurationService.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Repositories.Interfaces
{
    public interface IParametersBindingRepository
    {
        Task<IEnumerable<ParametersBingingEntity>> GetByProvider(Guid providerid);
        public Task<int> Add(ParametersBingingEntity entity);
        public Task Remove(int id);
    }
}
