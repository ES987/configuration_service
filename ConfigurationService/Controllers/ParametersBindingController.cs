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
        public async Task<Responce> Get([FromQuery] Guid providerId)
        {
            try
            {
                var res = await _repository.GetByProvider(providerId);
                return Responce.CreateSuccesResponce(res.Select(p => new ParametersBingingDto()
                {
                    Channel = p.Channel,
                    Id = p.Id,
                    ParameterId = p.ParameterId,
                    ProviderId = providerId
                }));
            }
            catch (Exception ex)
            {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }


        [HttpPost]
        public async Task<Responce> Add([FromBody] ParametersBingingDto dto)
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

                return Responce.CreateSuccesResponce(id);
            }
            catch (Exception ex)
            {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

        [HttpDelete]
        public async Task<Responce> Remove(int id) {
            try
            {
                await _repository.Remove(id);
                return Responce.CreateSuccesResponce();
            }
            catch (Exception ex) {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }
    }
}
