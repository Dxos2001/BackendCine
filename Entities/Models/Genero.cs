using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendCine.Entities.Models
{    
    [Table("Generos")]
    public class Genero : Control
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

    }
}