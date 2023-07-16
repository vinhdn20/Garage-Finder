using AutoMapper;
using DataAccess.DTO.Car;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarRepository carRepository;
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;
        public CarController(ICarRepository carRepository, IMapper mapper, IOrderRepository orderRepository)
        {
            this.carRepository = carRepository;
            this.mapper = mapper;
            this.orderRepository = orderRepository;
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult GetUserId()
        {
            var user = User.GetTokenInfor();
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
        [Authorize(Roles = Constants.ROLE_USER)]
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Add(AddCarDTO car)
        {
            try
            {
                var user = User.GetTokenInfor();
                if (!car.LicensePlates.IsValidLicensePlates())
                {
                    throw new Exception("License Plates not valid");
                }
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Update(UpdateCarDTO car)
        {
            try
            {
                var user = User.GetTokenInfor();
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
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Delete(int id)
        {
            try
            {
                var user = User.GetTokenInfor();
                if(orderRepository.GetAllOrdersByUserId(user.UserID).Any(x => x.CarID == id))
                {
                    throw new Exception("The car cannot be deleted. This car has been ordered");
                }
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
