using DataAccess.DTO;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services;
using Services.FavoriteListService;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteListController : ControllerBase
    {
        private readonly IFavoriteListService _favoriteListService;

        public FavoriteListController(IFavoriteListService favoriteListService)
        {
            this._favoriteListService = favoriteListService;
        }


        [HttpGet("GetByUser")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult GetUserId()
        {
            try
            {   
                var user = User.GetTokenInfor();
                return Ok(_favoriteListService.GetListByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add/{garageId}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult AddFavoriteGarage(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var listFV = _favoriteListService.GetListByUser(user.UserID);
                if(listFV.Any(x => x.GarageID == garageId))
                {
                    return BadRequest("Already contain garage");
                }
                var favoriteList = new FavoriteListDTO()
                {
                    GarageID = garageId,
                    UserID = user.UserID
                };
                _favoriteListService.Add(favoriteList);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("Delete/{garageId}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult DeleteFavoriteGarage(int garageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                _favoriteListService.Delete(garageId, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
    }
}
