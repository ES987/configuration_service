using ConfigurationService.Entities.Repositories.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Repositories.Helpers.Interfaces
{
    public interface IClientsHelper
    {
        public ClientEntity GetClient();
    }
}
