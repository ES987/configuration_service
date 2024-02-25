using ConfigurationService.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Repositories.Interfaces
{
    public interface IParametersRepository
    {
        Task<IEnumerable<ParameterEntity>> Get();
    }
}
