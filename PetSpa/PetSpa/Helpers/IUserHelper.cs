using Microsoft.AspNetCore.Identity;
using PetSpa.DAL.Entities;
using PetSpa.Models;

namespace PetSpa.Helpers
{
    public interface IUserHelper
    {
        // Firmas para metodos
        Task<User> GetUserAsync(string email); //captura los usuarios de la base de datos

        Task<IdentityResult> AddUserAsync(User user, string password); //Adiciona usuarios a la base de datos
        Task AddRoleAsync(string roleName); //Yo tengo los Roles: Admin y User, estos dos roles los va a agregar en la tabla AspNetRoles

        Task AddUserToRoleAsync(User user, string roleName); //Relaciona la tabla User with Roles, agrega un usuario nuevo y le asigna uno de los roles

        Task<bool> IsUserInRoleAsync(User user, string roleName); //Valida si un usuario pertenece a un Rol
        Task<SignInResult> LoginAsync(LoginViewModel loginViewModel);
        Task LogoutAsync();
    }
}
