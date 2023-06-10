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
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListRepository favoriteListRepository;

        public FavoriteListController(IFavoriteListRepository favoriteListRepository)
        {
            this.favoriteListRepository = favoriteListRepository;
        }

        private UsersDTO GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
            return user;
        }

        [HttpGet("GetByUser/{id}")]
        [Authorize]
        public IActionResult GetUserId()
        {
            try
            {
                var user = GetUserFromToken();
                return Ok(favoriteListRepository.GetListByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add(FavoriteListDTO favoriteList)
        {
            try
            {
                var user = GetUserFromToken();
                favoriteList.UserID = user.UserID;
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
