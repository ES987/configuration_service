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
        public async Task<Responce> Add()
        {
            try
            {
                var result = await _repository.Add();
                return Responce.CreateSuccesResponce(new AddProgramResponse()
                {
                    Id = result
                });
            }
            catch (Exception ex)
            {
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }


        [HttpGet]
        public async Task<Responce> GetAll()
        {
            try
            {
                var res = await _repository.GetAll();
                var resp = Responce.CreateSuccesResponce(res.Select(p => new ProgramDTO()
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
                return Responce.CreateFailResponce(ex.Message, ServiceError.Error);
            }
        }

        
    }
}
