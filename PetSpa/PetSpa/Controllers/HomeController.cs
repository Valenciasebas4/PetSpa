using Microsoft.AspNetCore.Mvc;
using PetSpa.DAL;
using PetSpa.Helpers;
using PetSpa.Models;
using System.Diagnostics;

namespace PetSpa.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _context;
        private readonly IUserHelper _userHelper;
  

        public HomeController(ILogger<HomeController> logger, DataBaseContext context, IUserHelper userHelper)
        {
            _logger = logger;
            _context = context;
            _userHelper = userHelper;
           
        }

        public IActionResult Index()
        {
            ViewBag.UserFullName = GetUserFullName();
            return View();
        }

        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}