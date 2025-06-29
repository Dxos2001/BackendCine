using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackendCine.Entities.Models
{
    [Table("Funciones")]
    public class Funcion : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Sala")]
        public int IdSala { get; set; }
        public Sala Sala { get; set; }

        [ForeignKey("Pelicula")]
        public int IdPelicula { get; set; }
        public Pelicula Pelicula { get; set; }

        [Column("Fecha")]
        public DateOnly Fecha { get; set; } // Fecha de la función

        [Column("HoraInicio")]
        public TimeSpan HoraInicio { get; set; } // Hora de inicio de la función

        [Column("HoraFin")]
        public TimeSpan HoraFin { get; set; } // Hora de fin de la función

        [Column("PrecioEntrada")]
        public decimal PrecioEntrada { get; set; } // Precio de la entrada

        [Column("AsientosDisponibles")]
        public int AsientosDisponibles { get; set; } // Número de asientos disponibles para la función

        [Column("Estado")]
        public string Estado { get; set; } // Puede ser "Programada", "En Curso", "Finalizada" o "Cancelada"

    }
}