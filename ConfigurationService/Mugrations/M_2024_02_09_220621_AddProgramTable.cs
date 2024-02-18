using FluentMigrator;
using ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository;
using static ConfigurationService.Entities.Repositories.Repositories.ProgramsRepository.Columns;
namespace ConfigurationService.Entities.Mugrations
{
    [Migration(2011091900220621, "добавлена таблица для хранения програм")]
    public class M_2024_02_09_220621_AddProgramTable : Migration
    {
        public override void Down()
        {
             
        }

        public override void Up()
        {
            string sql = $"CREATE TABLE IF NOT EXISTS {Repository.TableName} " +
   "(" +
      $"{Id}  uuid PRIMARY KEY," +
      $"{Description} text," +
      $"{ProgramType} text," +
      $"{DateCteate} int4" +
   "); ";
            Execute.Sql( sql);
        }
    }
}
