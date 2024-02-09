using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Entities.Repositories.Interfaces
{
    public interface ITableCreater
    {
        public Task Create();
    }
}
