using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Configs.Dto;
using ConfigurationService.Entities.Logic;
using ConfigurationService.Entities.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<Responce> Add([FromBody] ProviderDTO dto)
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

                return Responce.CreateSuccesResponce(id);
            }
            catch (Exception ex)
            {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }
        


        [HttpGet("ByProgram")]
        public async Task<Responce> GetByProgramId([FromQuery] Guid programId)
        {
            try
            {
                var res = await _repository.GetByProgramId(programId);
                return Responce.CreateSuccesResponce(res.Select(p => new ProviderDTO()
                {
                    Id = p.Id,
                    DataSource = p.DataSource,
                    Description = p.Description,
                    JsonConfig = p.JsonConfig,
                    ProgramId = p.ProgramId,
                    Type = p.Type,
                    IsStoped = p.IsStoped
                }));
            }
            catch (Exception ex)
            {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }
    }
}
