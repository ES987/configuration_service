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
        private IProvidersRepository _providersRepository;
        public Repository(IDbProvider provider, IProvidersRepository providersRepository)
        {
            _provider = provider;
            _providersRepository = providersRepository;
        }

        public Task<IEnumerable<ProgramEntity>> GetAll()
        {
            string sql = $"SELECT * FROM {TableName} ";
            return _provider.QueryAsync<ProgramEntity>(sql);

        }

        public async Task<Guid> Add()
        {

            Int32 unixTimestamp = (int)DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds;
            string sql = $"INSERT INTO {TableName} ({Id},{DateCteate} ) VALUES (@id,{unixTimestamp} ) RETURNING {Id}";
            var programId = Guid.NewGuid();
            await _provider.QueryAsync(sql, new
            {
                id = programId,
            });
            return programId;
        }
    }
}
