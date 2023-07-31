using DataAccess.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGarageRepository garageRepository;
        private readonly IAdminRepository adminRepository;
        private readonly IUsersRepository usersRepository;

        public AdminController(IGarageRepository garageRepository, IAdminRepository adminRepository,
            IUsersRepository usersRepository)
        {
            this.garageRepository = garageRepository;
            this.adminRepository = adminRepository;
            this.usersRepository = usersRepository;
        }

        [HttpPost("GetUsers")]
        [Authorize]
        public IActionResult GetAllUsers(KeyWordDTO keyword)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = adminRepository.GetAllUser();
                if (!string.IsNullOrEmpty(keyword.KeyWord))
                {
                    result = result.Where(g => g.Name.ToLower().Contains(keyword.KeyWord.ToLower())).ToList();
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpGet("GetUsersTotal")]
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

        [HttpPost("GetGarages")]
        [Authorize]
        public IActionResult GetAllGarage(KeyWordDTO keyword)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var garages = garageRepository.GetGarages();
                if (!string.IsNullOrEmpty(keyword.KeyWord))
                {
                    garages = garages.Where(g => g.GarageName.ToLower().Contains(keyword.KeyWord.ToLower())).ToList();
                }
                return Ok(garages);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        
        [HttpGet("GetGaragesTotal")]
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
        

        [HttpPost("SetStatusGarage")]
        [Authorize]
        public IActionResult SetStatusGarage(StatusGarageDTO garage)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.SetStatusGarage(garage);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("SetStatusUser")]
        [Authorize]
        public IActionResult SetStatusUser(StatusUserDTO user)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.SetStatusUser(user);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        private bool CheckAdmin()
        {
            var user = User.GetTokenInfor();
            var userDTO = usersRepository.GetUserByID(user.UserID);
            if (!userDTO.roleName.NameRole.Equals(Constants.ROLE_ADMIN))
            {
                return false;
            }
            return true;
        }
    }
}
