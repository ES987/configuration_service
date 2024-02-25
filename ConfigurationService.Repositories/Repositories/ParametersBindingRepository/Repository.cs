using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Repositories.Entities;
using ConfigurationService.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConfigurationService.Repositories.Repositories.ParametersBindingRepository.Columns;


namespace ConfigurationService.Repositories.Repositories.ParametersBindingRepository
{
    public class Repository : IParametersBindingRepository
    {
        public const string TableName = "parameters_binding";
        private IDbProvider _provider;
        public Repository(IDbProvider provider)
        {
            _provider = provider;
        }

        public Task<int> Add(ParametersBingingEntity entity)
        {
            string sql = $"INSERT INTO {TableName} ({ProviderId},{ParameterId},{Channel}) VALUES (@ProviderId,@Channel,@ParameterId) RETURNING {Id}";
            return _provider.QueryFirstOrDefaultAsync<int>(sql, entity);
        }

        public Task<IEnumerable<ParametersBingingEntity>> GetByProvider(Guid providerid)
        {
            string sql = $"SELECT * FROM {TableName} where {ProviderId} = @id";
            return _provider.QueryAsync<ParametersBingingEntity>(sql, new { id = providerid });

        }

        public Task Remove(int id)
        {
            string sql = $"DELETE FROM {TableName} WHERE {Id}  = @id";
            return _provider.ExecuteAsync(sql, new { id = id });
        }
    }
}
