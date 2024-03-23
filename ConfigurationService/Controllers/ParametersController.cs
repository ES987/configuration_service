using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Dto;
using ConfigurationService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ConfigurationService.Controllers
{
    [Route("Parameters")]
    public class ParametersController : Controller
    {
        private IParametersRepository _repository;
        public ParametersController(IParametersRepository repository)
        {
            _repository = repository;
        }


        [HttpGet]
        public async Task<Response> Get()
        {
            try {
                var res = await _repository.Get();
                return Api.Messages.Response.CreateSuccesResponce(res.Select(p => new ParameterDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                }));
            }
           catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce("", ServiceError.Error);
            }

        }

    }
}
