using GFData.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        public static List<Car> cars = new List<Car>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(cars);
        }
        [HttpPost]
        public IActionResult Create(Car car)
        {
            var carss = new Car();
            {

            }
            cars.Add(carss);
            return Ok(cars);
        }
    }
}
