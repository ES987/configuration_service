using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Dto;
using ConfigurationService.Logic.Handlers.Loki.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Controllers
{
    [Route("Logs")]
    public class LogsController : Controller
    {
        private ILokiHandler _handler;
        public LogsController(ILokiHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("ProviderLogs")]
        public async Task<Response> GetProviderLogs([FromQuery] Guid providerId, long start)
        {
            try
            {
                var res = await _handler.GetProviderLogs(providerId, start);
                return ConfigurationService.Api.Messages.Response.CreateSuccesResponce(res);
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("Ошибка при получении логов", ServiceError.Error);
            }


        }
    }
}
