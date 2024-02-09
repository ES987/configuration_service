using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationService.Database.Providers.Interfaces
{
    public interface IDbProvider
    {
        public Task<IEnumerable<T>> QueryAsync<T>(string sqlCommand, object obj = null);
        public Task ExecuteAsync(string sqlCommand, object obj = null);
        public Task<IEnumerable<dynamic>> QueryAsync(string sqlCommand, object obj = null);
        public Task<T> QueryFirstOrDefaultAsync<T>(string sqlCommand, object obj = null);
    }
}
