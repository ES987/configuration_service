using ConfigurationService.Entities.Configs.Dto;
using ConfigurationService.Entities.Configs.Requests;
using ConfigurationService.Entities.Requests;
using LoggerLib.Interfaces;
using MessagesLib.Entities;
using MessagesLib.Interfaces;
using MessagesLib.Senders;
using RequestHelpers.ConfigsHelpers.Events.ConfigsEventHandlers;
using RequestHelpers.ConfigsHelpers.Events.ConfigsEventHandlers.Interfaces;

namespace ConfigurationService.Entities.Logic
{
    public class Senders
    {
        private ISender _requestsSender;
        private IConfigsSender _configsSender;
        public Senders(RabbitConfiguration config, ILogger logger)
        {
            _requestsSender = new RabbitSender(new MessagesLib.Entities.RabbitConfiguration()
            {
                Exchange = "Requests",
                Host = config.Host,
                Password = config.Password,
                Port = config.Port,
                UserName = config.UserName
            });

            _configsSender = new ProviderRequestsSenderhandler(new MessagesLib.Entities.RabbitConfiguration()
            {
                Exchange = "Configs",
                Host = config.Host,
                Password = config.Password,
                Port = config.Port,
                UserName = config.UserName
            }, logger);
        }

        public Task SendRequest(ProviderRequest request)
        {
            return _requestsSender.Send(request);
        }


        public Task SendConfig(ProviderRequest request)
        {
            var providerInfo = request.Request as ProviderDTO;
            _configsSender.Send(new RequestHelpers.ConfigsHelpers.Entities.ProviderRequest()
            {

                ProgramId = request.ProgramId,
                RequestType = request.RequestType,
                Request = new RequestHelpers.ConfigsHelpers.Entities.Requests.ChangeProviderRequest()
                {
                    Config = request.Request,
                    DataSource = providerInfo.DataSource,
                    ProviderId = providerInfo.Id,
                    IsStoped = providerInfo.IsStoped,
                    Description = providerInfo.Description,
                    Type = providerInfo.Type,
                },

            });
            return Task.CompletedTask;
        }

        public Task UpdateDataSource(Guid appId, UpdateDataSourceRequest request)
        {
            _configsSender.Send(new RequestHelpers.ConfigsHelpers.Entities.ProviderRequest()
            {

                ProgramId = appId,
                Request = new RequestHelpers.ConfigsHelpers.Entities.Requests.UpdateDataSourceRequest()
                {
                    DataSource = request.DataSource,
                    ProviderId = request.ProviderId,
                },
                RequestType = RequestHelpers.ConfigsHelpers.Enums.ProviderRequestType.UpdateProviderDataSource
            });
            return Task.CompletedTask;
        }

        public Task RemoveProvider(Guid appId, Guid providerId)
        {
            _configsSender.Send(new RequestHelpers.ConfigsHelpers.Entities.ProviderRequest()
            {
                ProgramId = appId,
                RequestType = RequestHelpers.ConfigsHelpers.Enums.ProviderRequestType.RemoveProvider,
                Request = new RequestHelpers.ConfigsHelpers.Entities.Requests.RemoveProviderRequest()
                {
                    ProviderId = providerId
                }
            });
            return Task.CompletedTask;

        }

        public Task StopProvider(Guid appId, Guid providerId)
        {
            _configsSender.Send(new RequestHelpers.ConfigsHelpers.Entities.ProviderRequest()
            {
                ProgramId = appId,
                RequestType = RequestHelpers.ConfigsHelpers.Enums.ProviderRequestType.StopProvider,
                Request = new RequestHelpers.ConfigsHelpers.Entities.Requests.StopProviderRequest()
                {
                    ProviderId = providerId
                }
            });
            return Task.CompletedTask;
        }

        public Task StartProvider(Guid appId, Guid providerId)
        {
            _configsSender.Send(new RequestHelpers.ConfigsHelpers.Entities.ProviderRequest()
            {
                ProgramId = appId,
                RequestType = RequestHelpers.ConfigsHelpers.Enums.ProviderRequestType.StartProvider,
                Request = new RequestHelpers.ConfigsHelpers.Entities.Requests.StartProviderRequest()
                {
                    ProviderId = providerId
                }
            });
            return Task.CompletedTask;
        }
    }
}