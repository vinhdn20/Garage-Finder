using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace Garage_Finder_Backend.Controllers
{
    public class GarageController : Controller
    {
        private readonly IUsersRepository _userRepository;
        private readonly IRoleNameRepository _roleRepository;
        public GarageController(IUsersRepository usersRepository,
            IRoleNameRepository roleNameRepository)
        {
            _userRepository = usersRepository;
            _roleRepository = roleNameRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
