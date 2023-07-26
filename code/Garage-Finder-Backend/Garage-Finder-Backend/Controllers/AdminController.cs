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
        [Authorize(Roles = Constants.ROLE_ADMIN)]
        public IActionResult GetAllUsers(string? keyword)
        {
            try
            {
                var result = adminRepository.GetAllUser();
                if (!string.IsNullOrEmpty(keyword))
                {
                    result = result.Where(g => g.Name.ToLower().Contains(keyword.ToLower())).ToList();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpGet("GetUsersTotal")]
        [Authorize(Roles = Constants.ROLE_ADMIN)]
        public IActionResult GetTotalUser()
        {
            try
            {
                var result = adminRepository.GetAllUser().Count();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpGet("GetGarages")]
        [Authorize(Roles = Constants.ROLE_ADMIN)]
        public IActionResult GetAllGarage(string? keyword)
        {
            try
            {
                var garages = garageRepository.GetGarages();
                if (!string.IsNullOrEmpty(keyword))
                {
                    garages = garages.Where(g => g.GarageName.ToLower().Contains(keyword.ToLower())).ToList();
                }
                return Ok(garages);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpGet("GetGaragesTotal")]
        [Authorize(Roles = Constants.ROLE_ADMIN)]
        public IActionResult GetTotalGarage()
        {
            try
            {
                var result = garageRepository.GetGarages().Count();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }
        [HttpPost("BanUser")]
        public IActionResult BanUser(int id)
        {
            try
            {
                adminRepository.BanUser(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("UnBanUser")]
        public IActionResult UnBanUser(int id)
        {
            try
            {
                adminRepository.UnBanUser(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("BanGarage")]
        public IActionResult BanGarage(int id)
        {
            try
            {
                adminRepository.BanGarage(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("UnBanGarage")]
        public IActionResult UnBanGarage(int id)
        {
            try
            {
                adminRepository.UnBanGarage(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

    }
}
