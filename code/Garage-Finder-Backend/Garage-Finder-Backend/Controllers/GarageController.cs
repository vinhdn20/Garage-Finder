using DataAccess.DAO;
using DataAccess.DTO;
using DataAccess.DTO.RequestDTO;
using GFData.Models.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using Repositories.Interfaces;
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
        private readonly IGarageInforRepository inforRepository;
        public GarageController(IGarageRepository garageRepository, IGarageBrandRepository garageBrandRepository,
            ICategoryGarageRepository categoryGarageRepository, IImageGarageRepository  imageGarageRepository,
            IGarageInforRepository inforRepository)
        {
            this.garageRepository = garageRepository;
            this.garageBrandRepository = garageBrandRepository;
            this.categoryGarageRepository = categoryGarageRepository;
            this.imageGarageRepository = imageGarageRepository;
            this.inforRepository = inforRepository;
        }
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            try
            {
                return Ok(garageRepository.GetGarages());
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost("Add")]
        [Authorize]
        public IActionResult Add([FromBody] AddGarageDTO addGarage)
        {
            try
            {
                var user = GetUserFromToken();
                var garageDTO = garageRepository.Add(addGarage);

                GarageInfoDTO garageInfoDTO = new GarageInfoDTO()
                {
                    GarageID = garageDTO.GarageID,
                    UserID = user.UserID,
                };
                inforRepository.Add(garageInfoDTO);

                var listGarageBrand = new List<GarageBrandDTO>();
                foreach (var brand in addGarage.BrandsID)
                {
                    listGarageBrand.Add(new GarageBrandDTO()
                    {
                        GarageID = garageDTO.GarageID,
                        BrandID = brand
                    });
                }
                listGarageBrand.ForEach(x => garageBrandRepository.Add(x));

                var listCategory = new List<CategoryGarageDTO>();
                foreach (var cate in addGarage.CategoriesID)
                {
                    listCategory.Add(new CategoryGarageDTO()
                    {
                        CategoryID = cate,
                        GarageID = garageDTO.GarageID
                    });
                }
                listCategory.ForEach(x => categoryGarageRepository.Add(x));

                var listImage = new List<ImageGarageDTO>();
                foreach (var image in addGarage.ImageLink)
                {
                    listImage.Add(new ImageGarageDTO()
                    {
                        GarageID = garageDTO.GarageID,
                        ImageLink = image
                    });
                }
                listImage.ForEach(x => imageGarageRepository.AddImageGarage(x));
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


        [HttpGet("GetByKeyWord")]
        public IActionResult SearchGarage(string? keyword, string? location)
        {
            // Lấy danh sách garage từ nguồn dữ liệu
            var garages = garageRepository.GetGarages();

            // Áp dụng các bộ lọc
            if (!string.IsNullOrEmpty(keyword) && string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.GarageName.Contains(keyword)).ToList();
            }

            if (string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.AddressDetail.Contains(location)).ToList();
            }

            if (!string.IsNullOrEmpty(keyword) && !string.IsNullOrEmpty(location))
            {
                garages = garages.Where(g => g.GarageName.Contains(keyword) || g.AddressDetail.Contains(location)).ToList();
            }


            // Trả về kết quả
            return Ok(garages);
        }

        [HttpPost("GetByPage")]
        public IActionResult GetByPage([FromBody] PageDTO p)
        {
            var garages = garageRepository.GetByPage(p);
            return Ok(garages);
        }

        [HttpGet("GetByUser")]
        [Authorize]
        public IActionResult GetByUser()
        {
            try
            {
                var user = GetUserFromToken();
                return Ok(garageRepository.GetGarageByUser(user.UserID));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }
        #region Brand

        [HttpPost("addBrand")]
        [Authorize]
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

        [HttpPost("removeBrand")]
        [Authorize]
        public IActionResult RemoveBrand([FromBody] List<int> garageBrandIds)
        {
            try
            {
                foreach (var id in garageBrandIds)
                {
                    garageBrandRepository.Delete(id);
                }
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
        [Authorize]
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

        [HttpPost("removeCategory")]
        [Authorize]
        public IActionResult RemoveCategory([FromBody] List<int> ids)
        {
            try
            {
                foreach (var id in ids)
                {
                    categoryGarageRepository.Remove(id);
                }
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
        [Authorize]
        public IActionResult AddImage([FromBody] List<ImageGarageDTO> imagesDTO)
        {
            try
            {
                foreach (var image in imagesDTO)
                {
                    imageGarageRepository.AddImageGarage(image);
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("removeImage")]
        [Authorize]
        public IActionResult RemoveImage([FromBody] List<int> ids)
        {
            try
            {
                foreach(var id in ids)
                {
                    imageGarageRepository.RemoveImageGarage(id);
                }
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
                return Ok(garageRepository.GetGaragesByID(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetByFilter")]
        public IActionResult GetByFilter([FromBody] int id)
        {
            try
            {
                return Ok(garageRepository.GetGarageByProviceId(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public ActionResult GetGarages(
           [FromQuery] int? provinceID,
           [FromQuery] int? districtsID,
           [FromQuery] IEnumerable<int>? categoriesID,
           [FromQuery] IEnumerable<int>? brandsID)
        {
            List<GarageDTO> filteredGarages = garageRepository.GetGarages();
            List<GarageDTO> filteredByProvince = new List<GarageDTO>();
            List<GarageDTO> filteredByDistricts = new List<GarageDTO>();
            List<GarageDTO> filteredByCategory = new List<GarageDTO>();
            List<GarageDTO> filteredByBrand = new List<GarageDTO>();

            if (provinceID.HasValue)
            {
                filteredGarages = filteredGarages.Where(x => x.ProvinceID == provinceID).ToList();
            }

            if (districtsID.HasValue)
            {
                filteredGarages = filteredGarages.Where(x => x.DistrictsID == districtsID).ToList();
            }

            if (categoriesID != null && categoriesID.Any())
            {
                //filteredByCategory = filteredGarages.Where(x => x.ProvinceID == categoriesID).ToList();
            }

            if (brandsID != null && brandsID.Any())
            {
                //filteredByBrand = filteredGarages.Where(c => c.GarageBrands.Any(p => p.BrandID == id)).ToList();
            }

            filteredGarages.AddRange(filteredByProvince);
            filteredGarages.AddRange(filteredByDistricts);
            filteredGarages.AddRange(filteredByCategory);
            filteredGarages.AddRange(filteredByBrand);
            if (filteredGarages.Count == 0)
            {
                filteredGarages = garageRepository.GetGarages();
            }

            return Ok(filteredGarages);
        }
    }
}

        [HttpPost]
        public IActionResult GetGarageAround([FromBody] FindGarageAroundDTO findGarageAroundDTO)
        {
            try
            {
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
        #region Private
        private UsersDTO GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
            return user;
        }
        #endregion
    }
}
