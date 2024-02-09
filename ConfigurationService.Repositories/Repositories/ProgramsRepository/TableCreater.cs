using ConfigurationService.Database.Providers.Interfaces;
using ConfigurationService.Entities.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Columns;

namespace ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository
{
    internal class TableCreater : ITableCreater
    {

        public const string TableName = "programs";

        public IDbProvider _provider;

        public TableCreater(IDbProvider provider)
        {
            _provider = provider;
        }

        public async Task Create()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS {TableName} " +
   "(" +
      $"{Id}  uuid PRIMARY KEY," +
      $"{Description} text," +
      $"{ProgramType} text" +
   "); ";

            await _provider.QueryAsync(sql);

        }
    }
}
