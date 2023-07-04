using DataAccess.DTO.Category;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IOrderRepository orderRepository;

        public CategoryController(ICategoryRepository categoryRepository, IOrderRepository orderRepository)
        {
            this.categoryRepository = categoryRepository;
            this.orderRepository = orderRepository;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            return Ok(categoryRepository.GetCategory());
        }

        [HttpPost("Add")]
        public IActionResult Add(CategoryDTO category)
        {
            try
            {
                categoryRepository.Add(category);

                return Ok("Success");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult Update(CategoryDTO category)
        {
            try
            {
                categoryRepository.Update(category);

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
                categoryRepository.Delete(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
