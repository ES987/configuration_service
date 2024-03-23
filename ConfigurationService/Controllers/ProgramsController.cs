using ConfigurationService.Api.Messages;
using ConfigurationService.Entities.Configs;
using ConfigurationService.Entities.Configs.Dto;
using ConfigurationService.Entities.Configs.Responces;
using ConfigurationService.Entities.Repositories.Interfaces;
using IdentityModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ConfigurationService.Entities.Controllers
{

    [Route("Programs")]
    public class ProgramsController : Controller
    {
        IProgramsRepository _repository;
        public ProgramsController(IProgramsRepository repository)
        {
            _repository = repository;
        }

        [HttpPost]
        public async Task<Response> Add()
        {
            try
            {
                var result = await _repository.Add();
                return Api.Messages.Response.CreateSuccesResponce(new AddProgramResponse()
                {
                    Id = result
                });
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }


        [HttpGet]
        public async Task<Response> GetAll()
        {
            try
            {
                var res = await _repository.GetAll();
                var resp = Api.Messages.Response.CreateSuccesResponce(res.Select(p => new ProgramDTO()
                {
                    Id = p.Id,
                    DateCteate = p.DateCteate,
                    Description = p.Description,
                    ProgramType = p.ProgramType,
                }));
                return resp;
            }
            catch (Exception ex)
            {
                return Api.Messages.Response.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

        
    }
}
