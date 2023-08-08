using DataAccess.DTO.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using Services.AdminService;
using Services.GarageService;
using Services.UserService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IGarageService garageService;
        private readonly IAdminService adminService;
        private readonly IUserService _userService;

        public AdminController(IGarageService garageService, IAdminService adminService, IUserService userService)
        {
            this.garageService = garageService;
            this.adminService = adminService;
            this._userService = userService;
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
                var result = adminService.GetAllUser();
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
                var result = adminService.GetAllUser().Count();
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
                var garages = adminService.GetGarages();
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
                var result = adminService.GetGarages().Count();
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
                adminService.SetStatusGarage(garage);
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
                adminService.SetStatusUser(user);
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
            var userDTO = _userService.GetUserByID(user.UserID);
            if (!userDTO.roleName.NameRole.Equals(Constants.ROLE_ADMIN))
            {
                return false;
            }
            return true;
        }
    }
}
