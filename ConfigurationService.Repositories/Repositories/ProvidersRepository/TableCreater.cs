using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Repositories.Interfaces;
using static ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Columns;

namespace ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository
{
    public class TableCreater : ITableCreater
    {
        public const string TableName = "provider_configs";
        private IDbProvider _provider;
        public TableCreater(IDbProvider provider) { 
            _provider = provider;
        }
        public Task Create()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS {TableName} " +
    "(" +
       $"{Id}  uuid PRIMARY KEY," +
       $"{JsonConfig} jsonb NOT NULL," +
       $"{ProviderType}  text NOT NULL," +
       $"{DataSource} text," +
       $"{Description} text," +
       $"{ProgramId} uuid REFERENCES {ProgramsRepository.Repository.TableName} ({ProgramsRepository.Columns.Id})," +
       $"{IsStoped} boolean" +
    "); ";
            return _provider.QueryAsync(sql);
        }
    }
}
