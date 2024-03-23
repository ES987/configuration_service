using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Logic.Handlers.Providers.Interfaces
{
    public interface IHttpProvider
    {
        Task<T> Get<T>(string request);
    }
}
