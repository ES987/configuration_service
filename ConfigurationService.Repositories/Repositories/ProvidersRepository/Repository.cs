using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Repositories.Entities;
using ConfigurationService.Entities.Repositories.Interfaces;
using System.Text.Json;
using static ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Columns;
namespace ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository
{
    public class Repository : IProvidersRepository
    {
        public const string TableName = "provider_configs";
        private IDbProvider _provider;
        private string _tableName = TableCreater.TableName;
        public Repository(IDbProvider provider)
        {
            _provider = provider;
        }

        public Task<ProviderConfigEntity> GetProviderConfig(Guid id)
        {
            string sql = $"SELECT * FROM {_tableName} where id = @id";
            return _provider.QueryFirstOrDefaultAsync<ProviderConfigEntity>(sql, new { id = id });
        }

        public Task<IEnumerable<ProviderConfigEntity>> GetProviders()
        {
            string sql = $"SELECT * FROM {_tableName}";
            return _provider.QueryAsync<ProviderConfigEntity>(sql);
        }

        public async Task<Guid> Add(ProviderConfigEntity entity)
        {
            string json = string.Empty;

            json = JsonSerializer.Serialize(entity.JsonConfig);

            string sql = $"INSERT INTO {_tableName} ({Id}, {JsonConfig},{ProviderType},{DataSource},{Description},{ProgramId},{IsStoped}) " +
                $"VALUES " +
                $"(@Id,'{json}',@Type,@DataSource,@Description,@ProgramId,@IsStoped)";

            entity.Id = Guid.NewGuid();
            await _provider.QueryAsync(sql, new
            {
                Id = entity.Id,
                DataSource = entity.DataSource,
                Description = entity.Description,
                ProgramId = entity.ProgramId,
                Type = entity.Type.ToString(),
                IsStoped = entity.IsStoped,
            });
            return entity.Id;
        }



        public Task UpdateDataSource(Guid providerId, string dataSorce)
        {
            string sql = $"UPDATE {_tableName} SET {DataSource}  = @source where {Id} = @id";
            return _provider.ExecuteAsync(sql, new
            {
                source = dataSorce,
                id = providerId
            });
        }

        public Task StopProvider(Guid providerId)
        {
            string sql = $"UPDATE {_tableName} SET {IsStoped} = true where {Id} = @id";
            return _provider.ExecuteAsync(sql, new { id = providerId });
        }

        public Task<Guid> GetAppIdProvider(Guid providerId)
        {
            string sql = $"SELECT {ProgramId} FROM {_tableName} WHERE {Id} = @id";
            return _provider.QueryFirstOrDefaultAsync<Guid>(sql, new { id = providerId });
        }


        public Task<IEnumerable<ProviderConfigEntity>> GetByProgramId(Guid id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE {ProgramId} = @id";
            return _provider.QueryAsync<ProviderConfigEntity>(sql, new { id = id });
        }

        public Task StartProvider(Guid providerId)
        {
            string sql = $"UPDATE {_tableName} SET {IsStoped} = false where {Id} = @id";
            return _provider.ExecuteAsync(sql, new { id = providerId });
        }

        public Task RemoveProvider(Guid providerId)
        {
            string sql = $"DELETE FROM {_tableName} where {Id} = @id";
            return _provider.ExecuteAsync(sql, new { id = providerId });
        }
    }
}
