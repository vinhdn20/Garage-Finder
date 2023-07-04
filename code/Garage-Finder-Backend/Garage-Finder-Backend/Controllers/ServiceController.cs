using DataAccess.DTO.Services;
using DataAccess.DTO.Services.RequestSerivesDTO;
using DataAccess.DTO.Token;
using DataAccess.DTO.User.ResponeModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.ServiceService;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IService _service;

        public ServiceController(IService service)
        {
            this._service = service;
        }

        //[HttpGet("GetAll")]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        return Ok(serviceRepository.GetServices());
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpGet("GetById/{id}")]
        public IActionResult GetId(int id)
        {
            try
            {
                return Ok(_service.GetServiceById(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByCategory/{categoryGarageId}")]
        public IActionResult GetByCategoryId(int categoryGarageId)
        {
            try
            {
                return Ok(_service.GetServceByCategoryGarageId(categoryGarageId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Add(AddServiceDTO service)
        {
            try
            {
                var user = User.GetTokenInfor();
                var addService = _service.AddService(service, user.UserID);

                return Ok(addService);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Update(ServiceDTO service)
        {
            try
            {
                var user = User.GetTokenInfor();
                _service.UpdateService(service, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = User.GetTokenInfor();
                _service.DeleteServiceById(id, user.UserID);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
