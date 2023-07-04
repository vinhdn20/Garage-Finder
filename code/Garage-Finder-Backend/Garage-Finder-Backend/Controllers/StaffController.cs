using DataAccess.DAO;
using DataAccess.DTO.Staff;
using DataAccess.DTO.Token;
using DataAccess.DTO.User.RequestDTO;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.StaffService;
using System.Data;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        //[HttpGet("getById/{id}")]
        //[Authorize]
        //public IActionResult GetStaff(int id)
        //{
        //    try
        //    {
        //        StaffDTO staff = new StaffDTO();
        //        return Ok(staff);
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}

        [HttpGet("getByGarage/{garageId}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult GetStaffByGarage(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var staffs = _staffService.GetByGarageID(garageId, user.UserID);
                return Ok(staffs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("update")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult UpdateStaff(UpdateStaffDTO staffDTO)
        {
            try
            {
                var user = User.GetTokenInfor();
                _staffService.UpdateStaff(staffDTO, user.UserID);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult DeleteStaff(int id)
        {
            try
            {
                var user = User.GetTokenInfor();
                _staffService.DeleteStaff(id, user.UserID);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("block")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult UpdateStatus(BlockStaffDTO blockStaff)
        {
            try
            {
                var user = User.GetTokenInfor();
                _staffService.UpdateStatus(blockStaff, user.UserID);
                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("add")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Add(AddStaffDTO staff)
        {
            try
            {
                var user = User.GetTokenInfor();
                var staffAdd = _staffService.AddStaff(staff, user.UserID);
                return Ok(staffAdd);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public IActionResult Login(LoginModel model)
        {
            try
            {
                var staff = _staffService.LoginStaff(model);
                return Ok(staff);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("refresh-token")]
        [Authorize(Roles = $"{Constants.ROLE_STAFF}")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            try
            {
                var user = User.GetTokenInfor();
                return Ok(_staffService.RefreshToken(refreshToken, user.UserID));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("getMyStaffInfor")]
        [Authorize(Roles = $"{Constants.ROLE_STAFF}")]
        public IActionResult GetInfor()
        {
            try
            {
                var user = User.GetTokenInfor();
                return Ok(_staffService.GetMyInfor(user.UserID));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
