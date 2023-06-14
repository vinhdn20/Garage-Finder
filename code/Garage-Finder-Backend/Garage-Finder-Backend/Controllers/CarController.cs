using DataAccess.DTO;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository carRepository;
        #region Private
        private UsersDTO GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
            return user;
        }
        #endregion
        public CarController(ICarRepository carRepository)
        {
            this.carRepository = carRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(carRepository.GetCars());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetByUser")]
        [Authorize]
        public IActionResult GetUserId()
        {
            var user = GetUserFromToken();
            try
            {
                return Ok(carRepository.GetCarsByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add(CarDTO car)
        {
            try
            {
                var user = GetUserFromToken();
                car.UserID = user.UserID;
                carRepository.SaveCar(car);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(CarDTO car)
        {
            try
            {
                var user = GetUserFromToken();
                car.UserID = user.UserID;
                carRepository.UpdateCar(car);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        [Authorize]
        public IActionResult Delete(int id)
        {
            try
            {
                carRepository.DeleteCar(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
