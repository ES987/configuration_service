using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Dto;
using ConfigurationService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Controllers
{
    [Route("ParametersBinding")]
    public class ParametersBindingController : Controller
    {
        private IParametersBindingRepository _repository;
        public ParametersBindingController(IParametersBindingRepository repository)
        {
            _repository = repository;
        }


        [HttpGet("ByProvider")]
        public async Task<Response> Get([FromQuery] Guid providerId)
        {
            try
            {
                var res = await _repository.GetByProvider(providerId);
                return Api.Messages.Response.CreateSuccesResponce(res.Select(p => new ParametersBingingDto()
                {
                    Channel = p.Channel,
                    Id = p.Id,
                    ParameterId = p.ParameterId,
                    ProviderId = providerId
                }));
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }


        [HttpPost]
        public async Task<Response> Add([FromBody] ParametersBingingDto dto)
        {
            try
            {
                int id = await _repository.Add(new Repositories.Entities.ParametersBingingEntity()
                {
                    Channel = dto.Channel,
                    Id = dto.Id,
                    ParameterId = dto.ParameterId,
                    ProviderId = dto.ProviderId

                });

                return Api.Messages.Response.CreateSuccesResponce(id);
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

        [HttpDelete]
        public async Task<Response> Remove(int id) {
            try
            {
                await _repository.Remove(id);
                return Api.Messages.Response.CreateSuccesResponce();
            }
            catch (Exception ex) {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }
    }
}
