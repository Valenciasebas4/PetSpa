using Microsoft.AspNetCore.Mvc.Rendering;
using PetSpa.DAL.Entities;
using System.ComponentModel.DataAnnotations;

namespace PetSpa.Models
{
    public class AddPetViewModel 
    {


        [Display(Name = "Servicio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public Guid ServiceId { get; set; }
        public string ServiceName { get; set; }
        public IEnumerable<SelectListItem> Services { get; set; }
        [Display(Name = "Nombre")]
        public string Name { get; set; }

       

        
    }
}
