using ConfigsLoaders.Configs;

namespace ConfigsLoaders.Interfaces
{
    public interface IConfigsLoader
    {
        public RabbitConfiguration GetRabbitConfiguration();
        public DataBaseConfig GetDbConfiguration();
        public string GetLokiUrl();
    }
}
