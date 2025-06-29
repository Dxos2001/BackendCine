using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendCine.Entities.Models
{   
    [Table("Clientes")]
    public class Cliente : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Documento")]
        public string Documento { get; set; } // Puede ser DNI, Pasaporte, etc.

        [Column("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("Apellido")]
        [MaxLength(100)]
        public string Apellido { get; set; }

        [Column("FechaNacimiento")]
        public DateOnly FechaNacimiento { get; set; }

        [Column("Genero")]
        [MaxLength(10)]
        public string Genero { get; set; } // Puede ser "Masculino", "Femenino", "Otro"

        [Column("Estado")]
        public string Estado { get; set; } // Puede ser "Activo" o "Inactivo"
    }
}