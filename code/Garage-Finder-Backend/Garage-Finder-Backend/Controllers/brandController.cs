using DataAccess.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.BrandService;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IBrandService brandService;

        public BrandController(IBrandService brandService)
        {
            this.brandService = brandService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(brandService.GetBrand());
        }

        [HttpPost("Add")]
        public IActionResult Add(BrandDTO brand)
        {
            try
            {
                brandService.Add(brand);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(BrandDTO brand)
        {
            try
            {
                brandService.Update(brand);

                return Ok("Success");
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
                brandService.Delete(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
