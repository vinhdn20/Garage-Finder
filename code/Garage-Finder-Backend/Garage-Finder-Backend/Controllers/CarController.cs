using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.RequestDTO.Car;
using DataAccess.DTO.ResponeModels.User;
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
        private readonly IMapper mapper;
        #region Private
        private UserInfor GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UserInfor>(jsonUser);
            return user;
        }
        #endregion
        public CarController(ICarRepository carRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
        }

        //[HttpGet("GetAll")]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        return Ok(carRepository.GetCars());
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e.Message);
        //    }
        //}
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

        [HttpPost("GetByCarId/{id}")]
        [Authorize]
        public IActionResult GetByCarID(int id)
        {
            try
            {
                return Ok(carRepository.GetCarById(id));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add(AddCarDTO car)
        {
            try
            {
                var user = GetUserFromToken();
                CarDTO carDTO = mapper.Map<AddCarDTO, CarDTO>(car);
                carDTO.UserID = user.UserID;
                carDTO = carRepository.SaveCar(carDTO);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize]
        public IActionResult Update(UpdateCarDTO car)
        {
            try
            {
                var user = GetUserFromToken();
                CarDTO carDTO = mapper.Map<UpdateCarDTO, CarDTO>(car);
                carDTO.UserID = user.UserID;
                carRepository.UpdateCar(carDTO);
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
