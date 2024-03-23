using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Configs.Requests;
using ConfigurationService.Entities.Logic;
using MessagesLib.Entities;
using MessagesLib.Interfaces;
using Microsoft.AspNetCore.Mvc;
using RequestHelpers.ConfigsHelpers.Enums;

namespace ConfigurationService.Entities.Controllers
{
    [Route("providers/control")]
    public class ContolProviderController : Controller
    {
        private Senders _senders;
        public ContolProviderController(Senders senders)
        {
            _senders = senders;
        }

        /// <summary>
        /// включение/выключение
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("turnOn")]
        public async Task<Response> TurnOn([FromBody] ProviderTurnOnRequest request)
        {
            if (request.ProgramId == Guid.Empty)
            {
                return Api.Messages.Response.CreateFailResponce("Не указан id программы", ServiceError.BadRequest);
            }

            if (request.Request.ProviderId == Guid.Empty)
            {
                return Api.Messages.Response.CreateFailResponce("Не указан id провайдера", ServiceError.BadRequest);
            }

            if (request.RequestType == ProviderRequestType.Unknow)
            {
                return Api.Messages.Response.CreateFailResponce("Неизвестный тип запроса", ServiceError.BadRequest);
            }

            if (request.Request == null)
            {
                return Api.Messages.Response.CreateFailResponce("Запрос на включение/выключение null", ServiceError.BadRequest);
            }

            await _senders.SendRequest(new ProviderRequest()
            {
                ProgramId = request.ProgramId,
                //Request = request.Request,
                RequestType = request.RequestType,
            });
             
            return Api.Messages.Response.CreateSuccesResponce();
        }
    }
}
