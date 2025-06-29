using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace BackendCine.Entities.Models
{

    [Table("Reservas")]
    public class Reserva : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("codigoReserva")]
        public string CodigoReserva { get; set; }

        [ForeignKey("Cliente")]
        public int IdCliente { get; set; }

        public virtual Cliente Cliente { get; set; }

        [ForeignKey("Funcion")]
        public int IdFuncion { get; set; }

        public virtual Funcion Funcion { get; set; }

        [Column("FechaReserva")]
        public DateOnly FechaReserva { get; set; }

        [Column("CantidadAsientos")]
        public int CantidadAsientos { get; set; }

        [Column("PrecioTotal")]
        public decimal PrecioTotal { get; set; }
        
        [Column("Estado")]
        public string Estado { get; set; } // Puede ser "Pendiente", "Confirmada", "Cancelada"

        [Column("FechaExpiracion")]
        public DateOnly FechaExpiracion { get; set; }
    }
}