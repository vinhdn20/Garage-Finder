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
        public IActionResult Update([FromBody] GarageDTO garage)
        {
            try
            {
                garageRepository.Update(garage);
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
        [HttpPost("AddBrandForGarage")]
        [Authorize]
        public IActionResult AddBrandForGarage([FromBody] GarageBrandDTO garageBrandDTO)
        {
            try
            {
                garageBrandRepository.Add(garageBrandDTO);
                return Ok("SUCCESS");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("AddCategoryForGarage")]
        [Authorize]
        public IActionResult AddCategoryForGarage([FromBody] CategoryGarageDTO categoryGarageDTO)
        {
            try
            {
                categoryGarageRepository.Add(categoryGarageDTO);
                return Ok("SUCCESS");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
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
        private UsersDTO GetUserFromToken()
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<UsersDTO>(jsonUser);
            return user;
        }

        [HttpGet("GetByFilter")]
        public IActionResult GetByFilter([FromBody] int id)
        {
            try
            {
                return Ok(garageRepository.FilterByCity(id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
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
    }
}
