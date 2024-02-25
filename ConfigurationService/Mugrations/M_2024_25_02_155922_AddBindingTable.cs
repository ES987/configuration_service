using FluentMigrator;
using ConfigurationService.Repositories.Repositories.ParametersBindingRepository;
using ConfigurationService.Repositories.Repositories;

using static ConfigurationService.Repositories.Repositories.ParametersBindingRepository.Columns;
using System.Xml.Linq;

namespace ConfigurationService.Mugrations
{
    [Migration(20242502155922, "добавлена таблица параметров")]
    public class M_2024_25_02_155922_AddBindingTable : Migration
    {
        public override void Down()
        {
           
        }

        public override void Up()
        {
            Execute.Sql($"CREATE TABLE IF NOT EXISTS  {Repository.TableName} (" +
          $" {Id} SERIAL PRIMARY KEY, " +
          $" {ProviderId} uuid REFERENCES {ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Repository.TableName} ({ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Columns.Id})," +
          $" {ParameterId} int4 REFERENCES {ConfigurationService.Repositories.Repositories.Parameters.Repository.TableName} ({ConfigurationService.Repositories.Repositories.Parameters.Columns.Id})," +
          $" {Channel} int4 " +
      ");");
      
        }
    }
}
