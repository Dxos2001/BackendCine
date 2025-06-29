using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackendCine.Entities.Models
{
    [Table("ReservasXAsientos")]
    public class ReservaXAsientos : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Reserva")]
        public int IdReserva { get; set; }

        public virtual Reserva Reserva { get; set; }

        [Column("FechaReserva")]
        public DateOnly FechaReserva { get; set; }

        [Column("Estado")]
        public string Estado { get; set; } // Puede ser "Reservado" o "Ocupado"
    }
}