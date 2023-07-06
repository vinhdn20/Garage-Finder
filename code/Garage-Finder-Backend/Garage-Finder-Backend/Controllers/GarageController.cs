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
using Repositories.Interfaces;
using Services.GarageService;
using System.Security.Claims;

namespace Garage_Finder_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GarageController : Controller
    {
        private readonly IGarageRepository garageRepository;
        private readonly IGarageBrandRepository garageBrandRepository;
        private readonly ICategoryGarageRepository categoryGarageRepository;
        private readonly IImageGarageRepository imageGarageRepository;
        private readonly IGarageService garageService;
        private readonly ICategoryRepository categoryRepository;
        public GarageController(IGarageRepository garageRepository, IGarageBrandRepository garageBrandRepository,
            ICategoryGarageRepository categoryGarageRepository, IImageGarageRepository imageGarageRepository,
            IGarageService garageService, ICategoryRepository categoryRepository)
        {
            this.garageRepository = garageRepository;
            this.garageBrandRepository = garageBrandRepository;
            this.categoryGarageRepository = categoryGarageRepository;
            this.imageGarageRepository = imageGarageRepository;
            this.garageService = garageService;
            this.categoryRepository = categoryRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                var garages = garageRepository.GetGarages();
                foreach (var garage in garages)
                {
                    foreach (var cate in garage.CategoryGarages)
                    {
                        cate.CategoryName = categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                    }
                }
                return Ok(garages);
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
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
        public IActionResult Update([FromBody] UpdateGarageDTO garageUpdate)
        {
            try
            {
                garageRepository.Update(garageUpdate);
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
                garageRepository.DeleteGarage(id);
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
            var garages = garageRepository.GetGarages();

            // Áp dụng các bộ lọc
            /*if (!string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is null && searchGarage.districtsID is null && searchGarage.categoriesID is null)
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword)).ToList();
            }

            if (string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is not null && searchGarage.categoriesID is null)
            {
                garages = garages.Where(g => searchGarage.provinceID.Any(d => d == g.ProvinceID) && searchGarage.districtsID.Any(d => d == g.DistrictsID)).ToList();
            }

            if (string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is null && searchGarage.districtsID is null && searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }

            if (!string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is not null && searchGarage.categoriesID is null)
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword) && searchGarage.provinceID.Any(d => d == g.ProvinceID) && searchGarage.districtsID.Any(d => d == g.DistrictsID)).ToList();
            }

            if (!string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is null && searchGarage.districtsID is null && searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword) && g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }

            if (string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is not null && searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => searchGarage.provinceID.Any(d => d == g.ProvinceID) && searchGarage.districtsID.Any(d => d == g.DistrictsID) && g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }

            if (!string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is not null && searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword) && searchGarage.provinceID.Any(d => d == g.ProvinceID) && searchGarage.districtsID.Any(d => d == g.DistrictsID) && g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }

            if (string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is null && searchGarage.categoriesID is null)
            {
                garages = garages.Where(g => searchGarage.provinceID.Any(d => d == g.ProvinceID)).ToList();
            }

            if (!string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is null && searchGarage.categoriesID is null)
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword) && searchGarage.provinceID.Any(d => d == g.ProvinceID)).ToList();
            }

            if (string.IsNullOrEmpty(searchGarage.keyword) && searchGarage.provinceID is not null && searchGarage.districtsID is null && searchGarage.categoriesID is not null)
            {
                garages = garages.Where(g => searchGarage.provinceID.Any(d => d == g.ProvinceID) && g.CategoryGarages.Any(c => searchGarage.categoriesID.Any(x => x == c.CategoryID))).ToList();
            }*/

            if (!string.IsNullOrEmpty(searchGarage.keyword))
            {
                garages = garages.Where(g => g.GarageName.Contains(searchGarage.keyword)).ToList();
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
                    cate.CategoryName = categoryGarageRepository.GetById(cate.CategoryGarageID).CategoryName;
                }
            }
            // Trả về kết quả
            return Ok(garages);
        }

        [HttpPost("GetByPage")]
        public IActionResult GetByPage([FromBody] PageDTO p)
        {
            var garages = garageRepository.GetByPage(p);
            foreach (var garage in garages)
            {
                foreach (var cate in garage.CategoryGarages)
                {
                    cate.CategoryName = categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                }
            }
            return Ok(garages);
        }

        [HttpGet("GetByUser")]
        [Authorize(Roles = Constants.ROLE_USER)]
        public IActionResult GetByUser()
        {
            try
            {
                var user = User.GetTokenInfor();
                var garages = garageRepository.GetGarageByUser(user.UserID);
                foreach (var garage in garages)
                {
                    foreach (var cate in garage.CategoryGarages)
                    {
                        cate.CategoryName = categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
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
                    garageBrandRepository.Add(garage);
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
                garageBrandRepository.Delete(id);
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
                    categoryGarageRepository.Add(categoryGarageDTO);
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
                categoryGarageRepository.Remove(id);
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
                    imageGarageRepository.AddImageGarage(imageDTO);
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
                imageGarageRepository.RemoveImageGarage(id);
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
                var garage = garageRepository.GetGaragesByID(id);
                foreach (var cate in garage.CategoryGarages)
                {
                    cate.CategoryName = categoryRepository.GetCategory().Where(x => x.CategoryID == cate.CategoryID).SingleOrDefault().CategoryName;
                }
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
            List<GarageDTO> filteredGarages = garageRepository.GetGarages();
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
                filteredGarages = garageRepository.GetGarages();
            }

            return Ok(filteredGarages);
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