using System.ComponentModel.DataAnnotations;

namespace PetSpa.DAL.Entities
{
    public class PetDetails : Entity
    {
        [Display(Name = "Mascota.")]
        public virtual Pet Pet { get; set; }

        [Display(Name = "Fecha de entrega")]
        public virtual DateTime? DeliveryDate { get; set; }
    }
}
