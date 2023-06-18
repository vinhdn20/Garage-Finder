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
        private readonly IImageCarRepository imageCarRepository;
        private readonly IMapper mapper;
        #region Private
        private UserInfor GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UserInfor>(jsonUser);
            return user;
        }
        #endregion
        public CarController(ICarRepository carRepository, IImageCarRepository imageCarRepository, IMapper mapper)
        {
            this.carRepository = carRepository;
            this.imageCarRepository = imageCarRepository;
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

                foreach (var link in car.ImageLink)
                {
                    ImageCarDTO image = new ImageCarDTO()
                    {
                        CarID = carDTO.CarID,
                        ImageLink = link
                    };
                    imageCarRepository.AddImage(image);
                }

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

        [HttpPost("RemoveImageById")]
        [Authorize]
        public IActionResult RemoveImage([FromBody] List<int> imageIds)
        {
            try
            {
                foreach (var id in imageIds)
                {
                    imageCarRepository.RemoveImage(id);
                }
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddImageById")]
        [Authorize]
        public IActionResult AddImage([FromBody] List<ImageCarDTO> imageIds)
        {
            try
            {
                foreach (var id in imageIds)
                {
                    id.ImageId = 0;
                    imageCarRepository.AddImage(id);
                }
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
