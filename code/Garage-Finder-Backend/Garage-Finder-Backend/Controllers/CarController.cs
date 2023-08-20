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
using Services.CarService;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IOderService _oderService;
        private readonly IMapper mapper;
        public CarController(ICarService carService, IMapper mapper, IOderService oderService)
        {
            this._carService = carService;
            this.mapper = mapper;
            this._oderService = oderService;
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
                return Ok(_carService.GetCarsByUser(user.UserID));
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
                return Ok(_carService.GetCarById(id));
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
                    throw new Exception("Vui lòng nhập biển số theo định dạng Mã tỉnh - Mã huyện - Thứ tự đăng kí xe");
                }
                CarDTO carDTO = mapper.Map<AddCarDTO, CarDTO>(car);
                carDTO.UserID = user.UserID;
                carDTO = _carService.SaveCar(carDTO);

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
                if (!car.LicensePlates.IsValidLicensePlates())
                {
                    throw new Exception("Vui lòng nhập biển số theo định dạng Mã tỉnh - Mã huyện - Thứ tự đăng kí xe");
                }
                CarDTO carDTO = mapper.Map<UpdateCarDTO, CarDTO>(car);
                carDTO.UserID = user.UserID;
                _carService.UpdateCar(carDTO);
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
                if(_oderService.GetAllOrdersByUserId(user.UserID).Any(x => x.CarID == id))
                {
                    throw new Exception("Xe này đã sử dụng dịch vụ trên hệ thống vì vậy bạn không thể xóa xe này.");
                }
                _carService.DeleteCar(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
