using ConfigsLoaders.Configs;
using ConfigsLoaders.Interfaces;

namespace ConfigsLoaders
{
    public class EnvironmentLoader : IConfigsLoader
    {
        public DataBaseConfig GetDbConfiguration()
        {
            return new DataBaseConfig()
            {
                DataBase = Environment.GetEnvironmentVariable("postgres_database"),
                Host = Environment.GetEnvironmentVariable("postgres_host"),
                Password = Environment.GetEnvironmentVariable("postgres_password"),
                Port = int.Parse(Environment.GetEnvironmentVariable("postgres_port")),
                Username = Environment.GetEnvironmentVariable("postgres_username")
            };
        }

        public string GetLokiUrl()
        {
            return Environment.GetEnvironmentVariable("loki");
        }

        public RabbitConfiguration GetRabbitConfiguration()
        {
            return new RabbitConfiguration()
            {
                Host = Environment.GetEnvironmentVariable("rabbit_host"),
                Password = Environment.GetEnvironmentVariable("rabbit_password"),
                Port = int.Parse(Environment.GetEnvironmentVariable("rabbit_port")),
                UserName = Environment.GetEnvironmentVariable("rabbit_username"),
            };
        }
    }
}
