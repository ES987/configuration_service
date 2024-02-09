using Dapper;
using ConfigurationService.Database.Entities;
using ConfigurationService.Database.Providers.Interfaces;
using Npgsql;

namespace ConfigurationService.Entities.Providers.Providers
{
    public class DapperProvider : IDbProvider
    {
        private DataBaseConfig _config;
        public DapperProvider(DataBaseConfig config)
        {
            _config = config;

        }


        public async Task<IEnumerable<T>> QueryAsync<T>(string sqlCommand, object obj = null)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
            {
                var result = await connection.QueryAsync<T>(sqlCommand, obj);
                return result;
            }
        }

        public async Task<IEnumerable<dynamic>> QueryAsync(string sqlCommand, object obj = null)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
            {
                var result = await connection.QueryAsync(sqlCommand, obj);
                return result;
            }
        }

        public async Task ExecuteAsync(string sqlCommand, object obj = null)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(GetConnectionString()))
            {
                await connection.QueryAsync(sqlCommand, obj);
            }
        }

        public async Task<T> QueryFirstOrDefaultAsync<T>(string sqlCommand, object obj = null)
        {
            return (await QueryAsync<T>(sqlCommand, obj)).FirstOrDefault();
        }


        private string GetConnectionString()
        {
            return $"Host={_config.Host}:{_config.Port}; Database = {_config.DataBase}; Username={_config.Username};Password={_config.Password}";
        }
    }
}
