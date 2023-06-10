using DataAccess.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : Controller
    {
        private readonly IGarageRepository garageRepository;
        public GarageController(IGarageRepository garageRepository)
        {
            this.garageRepository = garageRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(garageRepository.GetGarages());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        //[Authorize]
        public IActionResult Add(GarageDTO garage)
        {
            try
            {
                garageRepository.SaveGarage(garage);
                //var garageDTO = garageRepository.SaveGarage(garage);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(GarageDTO garage)
        {
            try
            {
                garageRepository.Update(garage);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                garageRepository.DeleteGarage(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpGet("GetByKeyWord")]
        public IActionResult SearchGarage(string? keyword, string? location)
        {
            // Lấy danh sách garage từ nguồn dữ liệu
            var garages = garageRepository.GetGarages();

            // Áp dụng các bộ lọc
            if (!string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.GarageName.Contains(keyword)).ToList();
            }

            if (string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.AddressDetail.Contains(location)).ToList();
            }

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.GarageName.Contains(keyword) || g.AddressDetail.Contains(location)).ToList();
            }


            // Trả về kết quả
            return Ok(garages);
        }

        [HttpPost("GetByPage")]
        public IActionResult GetByPage(PageDTO p) 
        {
            var garages = garageRepository.GetByPage(p);
            return Ok(garages);
        }

        [HttpGet("GetByUser")]
        [Authorize]
        public IActionResult GetByUser()
        {
            try
            {
                var user = GetUserFromToken();
                return Ok(garageRepository.GetGarageByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetByID/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                return Ok(garageRepository.GetGaragesByID(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        private UsersDTO GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
            return user;
        }

        [HttpGet("GetByFilter")]
        public IActionResult GetByFilter(int id)
        {
            try
            {
                return Ok(garageRepository.FilterByCity(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
