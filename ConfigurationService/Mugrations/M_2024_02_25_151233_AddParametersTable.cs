using FluentMigrator;
using ConfigurationService.Repositories.Repositories.Parameters;
using static ConfigurationService.Repositories.Repositories.Parameters.Columns;
namespace ConfigurationService.Mugrations
{
    [Migration(20240225151235, "добавлена таблица параметров")]
    public class M_2024_02_25_151233_AddParametersTable : Migration
    {
        public override void Down()
        {
            
        }

        public override void Up()
        {
            List<string> commands = new List<string>();

            for (int i = 0; i < 200; i++) {
                commands.Add(GetInsertCommand($"параметр {i}"));
            }

            Execute.Sql($"CREATE TABLE IF NOT EXISTS  {Repository.TableName} (" +
                    $" {Id} SERIAL PRIMARY KEY, " +
                    $" {Name} varchar(200)" +
                ");" +
                $"{string.Join(";",commands.ToArray())}");
        }

        private string GetInsertCommand(string name) 
        {
            return $"INSERT INTO {Repository.TableName} ({Name}) VALUES ('{name}')" ;
        }

    }
}
