using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.DTO.Category;
using DataAccess.DTO.Garage;
using DataAccess.DTO.User.ResponeModels;
using GFData.Models.Entity;
using Mailjet.Client.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using Repositories.Implements.CategoryRepository;
using Repositories.Implements.Garage;
using Repositories.Interfaces;
using Services;
using Services.CategoryGarageService;
using Services.CategoryService;
using Services.GarageBrandService;
using Services.GarageService;
using Services.ImageGarageService;
using System.Net.Mail;
using System.Security.Claims;
using Twilio.Types;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : Controller
    {
        private readonly IGarageBrandService garageBrandService;
        private readonly ICategoryGarageService categoryGarageService;
        private readonly IImageGarageService imageGarageService;
        private readonly IGarageService garageService;
        private readonly ICategoryService categoryService;
        public GarageController(IGarageBrandService garageBrandService,
            ICategoryGarageService categoryGarageService, IImageGarageService imageGarageService,
            IGarageService garageService, ICategoryService categoryService)
        {
            this.garageBrandService = garageBrandService;
            this.categoryGarageService = categoryGarageService;
            this.imageGarageService = imageGarageService;
            this.garageService = garageService;
            this.categoryService = categoryService;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var garages = garageService.GetGarages().Where(g => g.Status == Constants.GARAGE_ACTIVE);
                foreach (var garage in garages)
                {
                    foreach (var cate in garage.CategoryGarages)
                    {
                        cate.CategoryName = categoryService.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                    }
                }
                return Ok(garages);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        [HttpGet("GetTotal")]
        public IActionResult GetTotalGarage()
        {
            try
            {
                var result = garageService.GetGarages().Where(g => g.Status== Constants.GARAGE_ACTIVE).Count();
                return Ok(result);
            }
            catch (Exception e)
            {

                return StatusCode(500, $"Đã xảy ra lỗi: {e.Message}");
            }
        }

        [HttpPost("Add")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Add([FromBody] AddGarageDTO addGarage)
        {
            try
            {
                var user = User.GetTokenInfor();
                garageService.Add(addGarage, user.UserID);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut("Update")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult Update([FromBody] UpdateGarageDTO garageUpdate)
        {
            try
            {
                var user = User.GetTokenInfor();
                var garage = garageService.GetGaragesByID(garageUpdate.GarageID);
                garage.AddressDetail = garageUpdate.AddressDetail;
                garage.CloseTime = garageUpdate.CloseTime;
                garage.DistrictsID = garageUpdate.DistrictsID;
                garage.EmailAddress = garageUpdate.EmailAddress;
                garage.GarageID = garageUpdate.GarageID;
                garage.GarageName = garageUpdate.GarageName;
                garage.LatAddress = garageUpdate.LatAddress;
                garage.LngAddress = garageUpdate.LngAddress;
                garage.OpenTime = garageUpdate.OpenTime;
                garage.PhoneNumber = garageUpdate.PhoneNumber;
                garage.ProvinceID = garageUpdate.ProvinceID;
                garage.Thumbnail = garageUpdate.Thumbnail;
                garage.UserID = user.UserID;
                garageService.Update(garage);
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
                garageService.DeleteGarage(id);
                return Ok("SUCCESS");
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }


        [HttpPost("GetByKeyWord")]
        public IActionResult SearchGarage([FromBody] SearchGarage searchGarage)
        {
            // Lấy danh sách garage từ nguồn dữ liệu
            var garages = garageService.GetGarages().Where(g => g.Status == Constants.GARAGE_ACTIVE);

            if (!string.IsNullOrEmpty(searchGarage.keyword))
            {
                garages = garages.Where(g => g.GarageName.ToLower().Contains(searchGarage.keyword.ToLower())).ToList();
            }

            if (searchGarage.provinceID is not null)
            {
                garages = garages.Where(g => searchGarage.provinceID.Any(d => d == g.ProvinceID)).ToList();
            }

            if (searchGarage.districtsID is not null)
            {
                garages = garages.Where(g => searchGarage.districtsID.Any(d => d == g.DistrictsID)).ToList();
            }

            if (searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }

            if (searchGarage.brandsID is not null)
            {
                garages = garages.Where(g => g.GarageBrands.Any(c => searchGarage.brandsID.Any(x => x == c.BrandID))).ToList();
            }

            garages = garages.Skip((searchGarage.pageNumber - 1) * searchGarage.pageSize).Take(searchGarage.pageSize).ToList();

            foreach (var garage in garages)
            {
                foreach (var cate in garage.CategoryGarages)
                {
                    cate.CategoryName = categoryGarageService.GetById(cate.CategoryGarageID).CategoryName;
                }
            }
            // Trả về kết quả
            return Ok(garages);
        }

       /* [HttpPost("GetByPage")]
        public IActionResult GetByPage([FromBody] PageDTO p)
        {
            var garages = garageService.GetByPage(p);
            foreach (var garage in garages)
            {
                foreach (var cate in garage.CategoryGarages)
                {
                    cate.CategoryName = categoryService.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                }
            }
            return Ok(garages);
        }
*/
        [HttpGet("GetByUser")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult GetByUser()
        {
            try
            {
                var user = User.GetTokenInfor();
                var garages = garageService.GetGarageByUser(user.UserID);
                foreach (var garage in garages)
                {
                    foreach (var cate in garage.CategoryGarages)
                    {
                        cate.CategoryName = categoryService.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                    }
                }
                return Ok(garages);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #region Brand

        [HttpPost("addBrand")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult AddBrandForGarage([FromBody] List<GarageBrandDTO> garageBrandsDTO)
        {
            try
            {
                foreach (var garage in garageBrandsDTO)
                {
                    garageBrandService.Add(garage);
                }
                return Ok("SUCCESS");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeBrand/{id}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult RemoveBrand(int id)
        {
            try
            {
                //foreach (var id in garageBrandIds)
                //{
                //    garageBrandRepository.Delete(id);
                //}
                garageBrandService.Delete(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Category

        [HttpPost("addCategoryForGarage")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult AddCategoryForGarage([FromBody] List<AddCategoryGarage> addCategoryGarageDTO)
        {
            try
            {
                foreach (var cate in addCategoryGarageDTO)
                {
                    CategoryGarageDTO categoryGarageDTO = new CategoryGarageDTO()
                    {
                        GarageID = cate.GarageID,
                        CategoryID = cate.CategoryID
                    };
                    categoryGarageService.Add(categoryGarageDTO);
                }

                return Ok("SUCCESS");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeCategory/{id}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult RemoveCategory(int id)
        {
            try
            {
                categoryGarageService.Remove(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion

        #region Image
        [HttpPost("addImage")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult AddImage([FromBody] List<AddImageGarageDTO> imagesDTO)
        {
            try
            {
                foreach (var image in imagesDTO)
                {
                    var imageDTO = new ImageGarageDTO() 
                    {
                        GarageID= image.GarageID,
                        ImageLink = image.ImageLink,
                    };
                    imageGarageService.AddImageGarage(imageDTO);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("removeImage/{id}")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult RemoveImage(int id)
        {
            try
            {
                //foreach (var id in ids)
                //{
                //    imageGarageRepository.RemoveImageGarage(id);
                //}
                imageGarageService.RemoveImageGarage(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Search/Filter

        [HttpGet("GetByID/{id}")]
        public IActionResult GetById(int id)
        {
            try
            {
                var garage = garageService.GetById(id);
                return Ok(garage);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("GetByFilter")]
        public ActionResult GetGarages([FromBody] FilterGarage filterGarage)
        {
            List<GarageDTO> filteredGarages = garageService.GetGarages();
            List<GarageDTO> filteredByProvince = new List<GarageDTO>();
            List<GarageDTO> filteredByDistricts = new List<GarageDTO>();
            List<GarageDTO> filteredByCategory = new List<GarageDTO>();
            List<GarageDTO> filteredByBrand = new List<GarageDTO>();

            if (filterGarage.provinceID is not null)
            {
                if (filterGarage.provinceID.Count != 0)
                    filteredGarages = filteredGarages.Where(x => filterGarage.provinceID.Any(d => d == x.ProvinceID)).ToList();
            }

            if (filterGarage.districtsID is not null)
            {
                if (filterGarage.districtsID.Count != 0)
                    filteredGarages = filteredGarages.Where(x => filterGarage.districtsID.Any(d => d == x.DistrictsID)).ToList();
            }

            if (filterGarage.categoriesID is not null)
            {
                if (filterGarage.categoriesID.Count != 0)
                    filteredGarages = filteredGarages.Where(x => filterGarage.categoriesID.Any(i => x.CategoryGarages.Any(c => c.CategoryID == i))).ToList();
            }

            if (filterGarage.brandsID is not null)
            {
                if (filterGarage.brandsID.Count != 0)
                    filteredGarages = filteredGarages.Where(x => filterGarage.brandsID.Any(i => x.GarageBrands.Any(c => c.BrandID == i))).ToList();
            }

            if (filterGarage.provinceID is null && filterGarage.districtsID is null &&
                filterGarage.categoriesID is null && filterGarage.brandsID is null)
            {
                filteredGarages = garageService.GetGarages();
            }

            return Ok(filteredGarages);
        }

        [HttpGet("getGarageSuggest")]
        public IActionResult GetGarageSuggest()
        {
            try
            {
                var garas =  garageService.GetGarageSuggest();
                return Ok(garas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //[HttpPost]
        //public IActionResult GetGarageAround([FromBody] FindGarageAroundDTO findGarageAroundDTO)
        //{
        //    try
        //    {
        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //}
        #endregion
    }
}