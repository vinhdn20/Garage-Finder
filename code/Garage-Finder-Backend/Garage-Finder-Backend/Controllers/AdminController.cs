using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGarageRepository garageRepository;
        private readonly IAdminRepository adminRepository; 

        public AdminController(IGarageRepository garageRepository, IAdminRepository adminRepository)
        {
            this.garageRepository = garageRepository;
            this.adminRepository = adminRepository;
        }

        [HttpGet("GetUsers")]
        //[Authorize(Roles = $"{Constants.ROLE_ADMIN}")]
        public IActionResult GetAllUsers()
        {
            try
            {
                var result = adminRepository.GetAllUser();
                return Ok(result);
            } catch
            {
                return BadRequest();
            }
            
        }

        [HttpGet("GetUsersTotal")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetTotalUser()
        {
            var result = adminRepository.GetAllUser().Count();
            return Ok(result);
        }

        [HttpGet("GetGarages")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetAllGarage()
        {
            try
            {
                var garages = garageRepository.GetGarages();
                return Ok(garages);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetGaragesTotal")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult GetTotalGarage()
        {
            var result = garageRepository.GetGarages().Count();
            return Ok(result);
        }


    }
}
