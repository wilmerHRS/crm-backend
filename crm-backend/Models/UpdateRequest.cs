using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace crm_backend.Models
{
    public class UpdateRequest
    {
        [MaxLength(50)]
        public string? Nombre { get; set; }

        [MaxLength(255)]
        public string? Direcciones { get; set; }


        [MaxLength(90)]
        public string? Telefonos { get; set; }

        [MaxLength(255)]
        public string? Contactos { get; set; }
    }
}
