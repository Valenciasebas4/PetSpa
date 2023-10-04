
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PetSpa.DAL.Entities
{
    public class Service : Entity
    {

        #region Properties
        [Display(Name = "Servicio.")]
        [MaxLength(50)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Name { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        [Display(Name = "Precio")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public decimal Price { get; set; }

        [Display(Name = "Mascotas")]
        public Guid PetId { get; set; }

       public List<Pet> Pets { get;set; }
       // public ICollection<Vehicle> Vehicles { get; set; }

        
        #endregion
    }
}
