using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Repositories.Entities;
using ConfigurationService.Entities.Repositories.Interfaces;
using System.Text.Json;
using static ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Columns;
namespace ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository
{
    public class Repository : IProvidersRepository
    {
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
            if (entity.JsonConfig != null) 
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
                Type = entity.Type,
                IsStoped = entity.IsStoped,
            });
            return entity.Id;
        }



        public Task<IEnumerable<ProviderConfigEntity>> GetByProgramId(Guid id)
        {
            string sql = $"SELECT * FROM {_tableName} WHERE {ProgramId} = @id";
            return _provider.QueryAsync<ProviderConfigEntity>(sql, new { id = id });
        }
    }
}
