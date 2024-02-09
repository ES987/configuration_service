using ConfigurationService.Entities.Configs.Requests;
using MessagesLib.Entities;
using MessagesLib.Interfaces;
using MessagesLib.Senders;

namespace ConfigurationService.Entities.Logic
{
    public class Senders
    {
        private ISender _requestsSender;
        private ISender _cinfigsSender;
        public Senders(RabbitConfiguration config)
        {
            _requestsSender = new RabbitSender(new MessagesLib.Entities.RabbitConfiguration()
            {
                Exchange = "Requests",
                Host = config.Host,
                Password = config.Password,
                Port = config.Port,
                UserName = config.UserName
            });

            _cinfigsSender = new RabbitSender(new MessagesLib.Entities.RabbitConfiguration()
            {
                Exchange = "Configs",
                Host = config.Host,
                Password = config.Password,
                Port = config.Port,
                UserName = config.UserName
            });
        }

        public Task SendRequest(ProviderRequest request)
        {
            return _requestsSender.Send(request);
        }


        public Task SendConfig(ProviderRequest request)
        {
            return _cinfigsSender.Send(request);
        }


    }
}