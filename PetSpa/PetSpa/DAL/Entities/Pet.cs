using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetSpa.DAL.Entities
{
    public class Pet : Entity
    {
        #region Properties

        [Display(Name = "Servicio.")]
        public virtual Service Service { get; set; }
        

        [Display(Name = "Propietario.")]
        //[Required(ErrorMessage = "El campo {0} es obligatorio")]
        public virtual User Owner { get; set; }

        [Display(Name = "Nombre de la Mascota")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Detalles Vehiculos")]
        public Guid PetDetailsId { get; set; }

        public List<PetDetails> PetsDetails { get; set; }
        #endregion
    }
}
