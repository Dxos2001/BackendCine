using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackendCine.Entities.Models
{
    [Table("Salas")]
    public class Sala : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Nombre")]
        [MaxLength(100)]
        public string Nombre { get; set; }

        [Column("Capacidad")]
        public int Capacidad { get; set; } // Número de asientos en la sala

        [Column("Filas")]
        public int Filas { get; set; }

        [Column("Columnas")]
        public int Columnas { get; set; }
    }
}