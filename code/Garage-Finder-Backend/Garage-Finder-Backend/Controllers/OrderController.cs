using AutoMapper;
using DataAccess.DTO;
using DataAccess.DTO.Orders;
using DataAccess.DTO.Orders.RequestDTO;
using DataAccess.DTO.Orders.ResponseDTO;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.OrderService;
using System.Data;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Garage_Finder_Backend.Controllers
{
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        //[HttpGet("GetAllOrder")]
        //public IActionResult GetAll()
        //{
        //    try
        //    {
        //        return Ok(orderRepository.GetAllOrders());
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
            try
            {
                var user =User.GetTokenInfor();
                var list = orderService.GetByUserId(user.UserID);
                return Ok(list);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetOrderByGFId/{GFOrderID}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetOrder(int GFOrderID)
        {
            try
            {
                var user =User.GetTokenInfor();
                object order = orderService.GetOrderByGFID(GFOrderID, user.UserID);
                return Ok(order);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetOrderByGarageId/{GarageId}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GetOrderByGarageId(int GarageId)
        {
            try
            {
                var user = User.GetTokenInfor();
                var list = orderService.GetOrderByGarageId(GarageId, user.UserID);
                return Ok(list);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        #region Add order
        [HttpPost("AddOrder")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult AddOrderWithCar([FromBody] AddOrderWithCarDTO newOrder)
        {

            try
            {
                orderService.AddOrderWithCar(newOrder);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddOrderFromGuest")]
        [AllowAnonymous]
        public IActionResult AddOrderFromGuest([FromBody] AddOrderFromGuestDTO newOrder)
        {
            try
            {
                orderService.AddOrderFromGuest(newOrder);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("AddOrderWithoutCar")]
        [Authorize(Roles = $"{Constants.ROLE_USER}")]
        public IActionResult AddOrderWithouCar([FromBody] AddOrderWithoutCarDTO newOrder)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.AddOrderWithoutCar(newOrder, user.UserID);

                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        #endregion
        #region Update order

        [HttpPost("GarageAcceptOrder/{GFOrderID}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GarageAcceptOrder(int GFOrderID)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.GarageAcceptOrder(GFOrderID, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GarageRejectOrder/{GFOrderID}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GarageRejectOrder(int GFOrderID)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.GarageRejectOrder(GFOrderID, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GarageDoneOrder")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GarageDoneOrder([FromBody]DoneOrderDTO doneOrder)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.GarageDoneOrder(doneOrder, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("GarageCancelOrder/{GFOrderID}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult GarageCancelOrder(int GFOrderID)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.GarageCancelOrder(GFOrderID, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("UserCancelOrder/{GFOrderID}")]
        [Authorize(Roles = $"{Constants.ROLE_USER}, {Constants.ROLE_STAFF}")]
        public IActionResult UserCancelOrder(int GFOrderID)
        {
            try
            {
                var user =User.GetTokenInfor();
                orderService.UserCancelOrder(user.UserID, GFOrderID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        #endregion

        //[HttpDelete("Delete/{id}")]
        //public IActionResult Delete([FromBody] int id)
        //{
        //    try
        //    {
        //        orderRepository.Delete(id);
        //        return Ok("SUCCESS");
        //    }
        //    catch (Exception e)
        //    {

        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
