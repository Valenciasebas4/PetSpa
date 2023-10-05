using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpa.DAL;
using PetSpa.Helpers;

namespace PetSpa.Controllers
{
    public class PetsDetailsController : Controller
    {
        private readonly DataBaseContext _context;

        private readonly IUserHelper _userHelper;

        public PetsDetailsController(DataBaseContext context, IUserHelper userHelper)
        {
            _context = context;

            _userHelper = userHelper;

        }


        private string GetUserId()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.Id)
                .FirstOrDefault();
        }


        private string GetUserFullName()
        {
            return _context.Users
                .Where(u => u.Email == User.Identity.Name)
                .Select(u => u.FullName)
                .FirstOrDefault();
        }


        public async Task<IActionResult> Index()
        {
            ViewBag.UserFullName = GetUserFullName();


            ViewBag.UserId = GetUserId();


            return View(await _context.PetsDetails
                .Include(o => o.Pet.Owner)
                .Include(o => o.Pet.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.VehiclesDetails'  is null.");

        }

        public async Task<IActionResult> MyServices()
        {
            ViewBag.UserFullName = GetUserFullName();


            ViewBag.UserId = GetUserId();


            return View(await _context.PetsDetails
                .Include(o => o.Pet.Owner)
                .Include(o => o.Pet.Service)
                .ToListAsync());
            Problem("Entity set 'DataBaseContext.VehiclesDetails'  is null.");

        }
    }
}
