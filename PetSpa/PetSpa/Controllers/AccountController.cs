using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PetSpa.DAL;
using PetSpa.DAL.Entities;
using PetSpa.Enum;
using PetSpa.Helpers;
using PetSpa.Models;

namespace PetSpa.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;
        private readonly DataBaseContext _context;
        
       

        public AccountController(IUserHelper userHelper, DataBaseContext context)
        {
            _userHelper = userHelper;
            _context = context;
            
            
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


            return View();
        }


        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(loginViewModel);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos.");
            }
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }



        public IActionResult Unauthorized()
        {
            return View();
        }

        //Get- Vista para registrar Usuario
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            Guid emptyGuid = new Guid(); // Se crea un nuevo Guid

            AddUserViewModel addUserViewModel = new()
            {
                // Se precarga la vista con un ID nuevo, pais, estado, ciudad y tipo de usuario User
                Id = Guid.Empty,               
                UserType = UserType.User, //Usuarios registrados por pantalla son User
            };

            return View(addUserViewModel);
        }

        // Action que envia los datos capturados desde la vista 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel addUserViewModel)
        {
            if (ModelState.IsValid)
            {
                
                addUserViewModel.CreatedDate = DateTime.Now;

                User user = await _userHelper.AddUserAsync(addUserViewModel);
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Este correo ya está siendo usado.");
                    
                    return View(addUserViewModel);
                }

                //Autologeamos al nuevo usuario que se registra
                LoginViewModel loginViewModel = new()
                {
                    Password = addUserViewModel.Password,
                    RememberMe = false,
                    Username = addUserViewModel.Username
                };

                var login = await _userHelper.LoginAsync(loginViewModel);

                if (login.Succeeded) return RedirectToAction("Index", "Home");
            }

            return View(addUserViewModel);
        }


        // Get - vista de editar Usuario
        public async Task<IActionResult> EditUser()
        {
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            ViewBag.UserFullName = GetUserFullName();
            if (user == null) return NotFound();

            EditUserViewModel editUserViewModel = new()
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
               
                Id = Guid.Parse(user.Id),
                Document = user.Document
            };

            return View(editUserViewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUser(EditUserViewModel editUserViewModel)
        {
            if (ModelState.IsValid)
            {
                

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                user.FirstName = editUserViewModel.FirstName;
                user.LastName = editUserViewModel.LastName;
                user.Address = editUserViewModel.Address;
                user.PhoneNumber = editUserViewModel.PhoneNumber;
               
                user.Document = editUserViewModel.Document;

                IdentityResult result = await _userHelper.UpdateUserAsync(user);
                if (result.Succeeded) return RedirectToAction("Index", "Home");
                else ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
            }

            

            return View(editUserViewModel);
        }

        public IActionResult ChangePassword()
        {
            ViewBag.UserFullName = GetUserFullName();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                if (changePasswordViewModel.OldPassword == changePasswordViewModel.NewPassword)
                {
                    ModelState.AddModelError(string.Empty, "Debes ingresar una contraseña diferente.");
                    return View(changePasswordViewModel);
                }

                User user = await _userHelper.GetUserAsync(User.Identity.Name);

                if (user != null)
                {
                    IdentityResult result = await _userHelper.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.NewPassword);
                    if (result.Succeeded) return RedirectToAction("EditUser");
                    else ModelState.AddModelError(string.Empty, result.Errors.FirstOrDefault().Description);
                }
                else ModelState.AddModelError(string.Empty, "Usuario no encontrado");
            }

            return View(changePasswordViewModel);
        }

        public async Task<IActionResult> UserProfile()
        {
            // Obtener el usuario actual
            User user = await _userHelper.GetUserAsync(User.Identity.Name);
            ViewBag.UserFullName = GetUserFullName();
            if (user == null)
            {
                return NotFound();
            }



            // Preparar los datos necesarios para la vista
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel
            {
                Address = user.Address,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,             
                Id = Guid.Parse(user.Id),
                Document = user.Document,

            };

            // Resto del código...

            // Retornar la vista con los datos
            return View(userProfileViewModel);
        }

    }
}
