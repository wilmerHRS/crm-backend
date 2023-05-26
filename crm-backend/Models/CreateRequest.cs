using System.ComponentModel.DataAnnotations;

namespace crm_backend.Models
{
    public class CreateRequest
    {
        [Required]
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Direcciones { get; set; }


        [Required]
        [MaxLength(90)]
        public string? Telefonos { get; set; }

        [Required]
        [MaxLength(255)]
        public string? Contactos { get; set; }
    }
}
