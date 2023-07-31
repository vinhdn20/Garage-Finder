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

        [HttpGet("GetUsers")]
        [Authorize]
        public IActionResult GetAllUsers(string? keyword)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
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
        public IActionResult GetTotalUser()
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = adminRepository.GetAllUser().Count();
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }

        }

        [HttpGet("GetGarages")]
        [Authorize]
        public IActionResult GetAllGarage(string? keyword)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var garages = garageRepository.GetGarages().Where(g => g.Status == Constants.GARAGE_ACTIVE || g.Status ==Constants.GARAGE_LOCKED);
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

        [HttpGet("ConfirmGarages")]
        [Authorize]
        public IActionResult ConfirmGarages(string? keyword)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var garages = garageRepository.GetGarages().Where(g => g.Status == Constants.GARAGE_WAITING || g.Status == Constants.GARAGE_DENIED);
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
        [Authorize]
        public IActionResult GetTotalGarage()
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = garageRepository.GetGarages().Where(g => g.Status == Constants.GARAGE_ACTIVE || g.Status == Constants.GARAGE_LOCKED).Count();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }
        [HttpGet("GetTotalGarageConfirm")]
        [Authorize]
        public IActionResult GetTotalGarageConfirm()
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                var result = garageRepository.GetGarages().Where(g => g.Status == Constants.GARAGE_WAITING || g.Status == Constants.GARAGE_DENIED).Count();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }
        [HttpPost("BanUser")]
        [Authorize]
        public IActionResult BanUser(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.BanUser(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("UnBanUser")]
        [Authorize]
        public IActionResult UnBanUser(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.UnBanUser(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("BanGarage")]
        [Authorize]
        public IActionResult BanGarage(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.BanGarage(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("UnBanGarage")]
        [Authorize]
        public IActionResult UnBanGarage(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.UnBanGarage(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("AcceptGarage")]
        [Authorize]
        public IActionResult AcceptGarage(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.AcceptGarage(id);
                return Ok();
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("DeniedGarage")]
        [Authorize]
        public IActionResult DeniedGarage(int id)
        {
            try
            {
                if (!CheckAdmin())
                {
                    return Unauthorized("Bạn không phải là admin của web");
                }
                adminRepository.DeniedGarage(id);
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
