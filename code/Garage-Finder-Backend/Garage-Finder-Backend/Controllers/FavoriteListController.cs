using DataAccess.DTO;
using DataAccess.DTO.User.ResponeModels;
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


        [HttpGet("GetByUser")]
        [Authorize]
        public IActionResult GetUserId()
        {
            try
            {   
                var user = User.GetTokenInfor();
                return Ok(favoriteListRepository.GetListByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add/{garageId}")]
        [Authorize]
        public IActionResult AddFavoriteGarage(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var listFV = favoriteListRepository.GetListByUser(user.UserID);
                if(listFV.Any(x => x.GarageID == garageId))
                {
                    return BadRequest("Already contain garage");
                }
                var favoriteList = new FavoriteListDTO()
                {
                    GarageID = garageId,
                    UserID = user.UserID
                };
                favoriteListRepository.Add(favoriteList);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{garageId}")]
        public IActionResult DeleteFavoriteGarage(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                favoriteListRepository.Delete(garageId, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
