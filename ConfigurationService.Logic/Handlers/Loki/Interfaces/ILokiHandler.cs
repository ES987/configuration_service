using ConfigurationService.Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Logic.Handlers.Loki.Interfaces
{
    public interface ILokiHandler
    {
        Task<List<ProviderLogDto>> GetProviderLogs(Guid providerId,long start);
    }
}
