using PetSpa.DAL.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PetSpa.Helpers
{
    public interface IDropDownListHelper
    {
        Task<IEnumerable<SelectListItem>> GetDDLServicesAsync();

    }
}
