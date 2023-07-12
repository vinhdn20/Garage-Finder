using Microsoft.AspNetCore.Mvc;

namespace Garage_Finder_Backend.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
