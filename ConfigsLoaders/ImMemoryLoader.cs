using ConfigsLoaders.Configs;
using ConfigsLoaders.Interfaces;


namespace ConfigsLoaders
{
    public class ImMemoryLoader : IConfigsLoader
    {
        private string _ipServer = "192.168.73.129";
        public ImMemoryLoader()
        {

        }

        public DataBaseConfig GetDbConfiguration()
        {
            return new DataBaseConfig()
            {
                Host = _ipServer,
                Password = "example",
                Port = 5432,
                Username = "postgres",
                
            };
        }

        public RabbitConfiguration GetRabbitConfiguration()
        {
            return new RabbitConfiguration()
            {
                Host = _ipServer,
                Password = "guest",
                Port = 5672,
                UserName = "guest"
            };
        }


    }
}
