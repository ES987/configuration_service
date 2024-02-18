using ConfigurationService.Entities.Repositories.Interfaces;
using FluentMigrator;
using RequestHelpers.ConfigsHelpers.Enums;
using ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository;
using static ConfigurationService.Entities.Repositories.Repositories.ProvidersRepository.Columns;
namespace ConfigurationService.Mugrations
{
    [Migration(2011091900220622, "добавлена таблица для хранения првайдеров")]
    public class M_2024_02_09_220622_AppProvidersTable : Migration
    {
        public override void Down()
        {
             
        }

        public override void Up()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS {Repository.TableName} " +
    "(" +
            $"{Id}  uuid PRIMARY KEY," +
            $"{JsonConfig} jsonb NOT NULL," +
            $"{Columns.ProviderType}  text NOT NULL," +
            $"{DataSource} text," +
            $"{Description} text," +
            $"{ProgramId} uuid REFERENCES {ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Repository.TableName} ({ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Columns.Id})," +
            $"{IsStoped} boolean" +
    "); ";
            Execute.Sql(sql);
        }
    }
}
