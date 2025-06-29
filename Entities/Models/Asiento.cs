using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackendCine.Entities.Models
{

    [Table("Asientos")]
    public class Asiento : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Sala")]
        public int IdSala { get; set; }
        public Sala Sala { get; set; }

        [Column("Fila")]
        public string Fila { get; set; }

        [Column("Columna")]
        public int Columna { get; set; }

        [Column("Estado")]
        public string Estado { get; set; } // Puede ser "Disponible", "Reservado" o "Ocupado"
    }
}