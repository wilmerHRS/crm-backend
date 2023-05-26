using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace crm_backend.Entities
{
    public class Customer : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdCustomer { get; set; }

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

        [JsonIgnore]
        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
