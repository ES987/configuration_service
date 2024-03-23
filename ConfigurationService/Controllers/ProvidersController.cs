using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Configs.Dto;
using ConfigurationService.Entities.Logic;
using ConfigurationService.Entities.Repositories.Interfaces;
using ConfigurationService.Entities.Requests;
using HomePlatform.Logic.Entities;
using Microsoft.AspNetCore.Mvc;

using RequestHelpers.ConfigsHelpers.Enums;
using System.Text.Json;

namespace ConfigurationService.Entities.Controllers
{
    [Route("Providers")]
    public class ProvidersController : Controller
    {
        private IProvidersRepository _repository;
        private Senders _senders;
        public ProvidersController(IProvidersRepository repository, Senders senders)
        {
            _repository = repository;
            _senders = senders;
        }

        [HttpPost]
        public async Task<Response> Add([FromBody] ProviderDTO dto)
        {
            try
            {
                var id = await _repository.Add(new Repositories.Entities.ProviderConfigEntity()
                {
                    DataSource = dto.DataSource,
                    Description = dto.Description,
                    Id = dto.Id,
                    JsonConfig = dto.JsonConfig,
                    ProgramId = dto.ProgramId,
                    Type = dto.Type,
                });
                dto.Id = id;
                dto.IsStoped = true;
                await _senders.SendConfig(new Configs.Requests.ProviderRequest()
                {
                    ProgramId = dto.ProgramId,
                    RequestType = ProviderRequestType.AddProvider,
                    Request = dto
                });

                return Api.Messages.Response.CreateSuccesResponce(id);
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

        [HttpPut("UpdateDataSouce")]
        public async Task<Response> UpdateDataSouce([FromQuery] UpdateDataSourceRequest request)
        {
            try
            {
                Guid appid = await _repository.GetAppIdProvider(request.ProviderId);
                if (Guid.Empty != appid)
                {
                    await _repository.UpdateDataSource(request.ProviderId, request.DataSource);
                    await _senders.UpdateDataSource(appid, request);
                }

                return Api.Messages.Response.CreateSuccesResponce();
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("", ServiceError.Error);
            }

        }

        [HttpPost("StopProvider")]
        public async Task<Response> StopProvider(Guid providerId)
        {
            try
            {

                Guid appid = await _repository.GetAppIdProvider(providerId);
                if (Guid.Empty != appid)
                {
                    await _repository.StopProvider(providerId);
                    await _senders.StopProvider(appid, providerId);
                }
                return Api.Messages.Response.CreateSuccesResponce();
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("", ServiceError.Error);
            }
        }

        [HttpPost("StartProvider")]
        public async Task<Response> StartProvider(Guid providerId)
        {
            try
            {
                Guid appid = await _repository.GetAppIdProvider(providerId);
                if (Guid.Empty != appid)
                {
                    await _repository.StartProvider(providerId);
                    await _senders.StartProvider(appid, providerId);
                }
                return Api.Messages.Response.CreateSuccesResponce();
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("", ServiceError.Error);
            }
        }

        [HttpDelete]
        public async Task<Response> Remove(Guid providerId)
        {
            try
            {

                Guid appid = await _repository.GetAppIdProvider(providerId);
                if (Guid.Empty != appid)
                {
                    await _repository.RemoveProvider(providerId);
                    await _senders.RemoveProvider(appid, providerId);
                }
                return Api.Messages.Response.CreateSuccesResponce();
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("", ServiceError.Error);
            }
        }

        [HttpGet("ByProgram")]
        public async Task<Response> GetByProgramId([FromQuery] Guid programId)
        {
            try
            {
                var res = await _repository.GetByProgramId(programId);

                foreach (var item in res)
                {
                    switch (item.Type) {
                        case ProviderType.Arduino:
                            item.JsonConfig = JsonSerializer.Deserialize<ArduinoConfig>(item.JsonConfig.ToString());
                            break;
                    }
                }

                return Api.Messages.Response.CreateSuccesResponce(res.Select(p => new ProviderDTO()
                {
                    Id = p.Id,
                    DataSource = p.DataSource,
                    Description = p.Description,
                    JsonConfig = p.JsonConfig,
                    ProgramId = p.ProgramId,
                    Type = p.Type,
                    IsStoped = p.IsStoped,

                }));
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

         
    }
}
