using DataAccess.DTO;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListRepository favoriteListRepository;

        public FavoriteListController(IFavoriteListRepository favoriteListRepository)
        {
            this.favoriteListRepository = favoriteListRepository;
        }

        [HttpGet("GetByUser/{id}")]
        public IActionResult GetUserId(int id)
        {
            try
            {
                return Ok(favoriteListRepository.GetListByUser(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        public IActionResult Add(FavoriteListDTO favoriteList)
        {
            try
            {
                favoriteListRepository.Add(favoriteList);

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
                favoriteListRepository.Delete(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
