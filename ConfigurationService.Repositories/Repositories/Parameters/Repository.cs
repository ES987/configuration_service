using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Repositories.Entities;
using ConfigurationService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Repositories.Repositories.Parameters
{
    public class Repository : IParametersRepository
    {
        public const string TableName = "parameters";
        private IDbProvider _provider;
        public Repository(IDbProvider provider)
        {
            _provider = provider;
        }
        public Task<IEnumerable<ParameterEntity>> Get()
        {
            string sql = $"SELECT * FROM {TableName}";
            return _provider.QueryAsync<ParameterEntity>(sql);
        }
    }
}
