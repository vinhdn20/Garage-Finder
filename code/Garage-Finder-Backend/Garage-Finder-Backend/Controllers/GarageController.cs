using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

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
        public IActionResult Add(GarageDTO garage)
        {
            try
            {
                garageRepository.SaveGarage(garage);
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
                garages = garages.Where(g => g.Address.Contains(location)).ToList();
            }

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.GarageName.Contains(keyword) || g.Address.Contains(location)).ToList();
            }


            // Trả về kết quả
            return Ok(garages);
        }
    }
}
