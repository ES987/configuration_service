using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Repositories.Entities;
using ConfigurationService.Entities.Repositories.Interfaces;
using static ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Columns;


namespace ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository
{
    public class Repository : IProgramsRepository
    {
        public const string TableName = "programs";
        private IDbProvider _provider;
        public Repository(IDbProvider provider)
        {
            _provider = provider;
        }

        public Task<IEnumerable<ProgramEntity>> GetAll()
        {
            string sql = $"SELECT * FROM {TableName}";
            return _provider.QueryAsync<ProgramEntity>(sql);
        }
        public async Task<Guid> Add(UserInfo user)
        {
            string sql = $"INSERT INTO {TableName} ({Id} ) VALUES (@id ) RETURNING {Id}";
            var programId = Guid.NewGuid();
            await _provider.QueryAsync(sql, new
            {
                id = programId,
            });
            return programId;
        }
    }
}
