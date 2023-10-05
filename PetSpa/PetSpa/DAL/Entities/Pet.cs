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
        [MaxLength(15)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Display(Name = "Detalles MAscotas")]
        public Guid PetDetailsId { get; set; }

        public List<PetDetails> PetsDetails { get; set; }
        #endregion
    }
}
