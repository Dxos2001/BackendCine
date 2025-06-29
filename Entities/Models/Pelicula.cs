using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendCine.Entities.Models
{
    [Table("Peliculas")]
    public class Pelicula : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column("Titulo")]
        [MaxLength(200)]
        public string Titulo { get; set; }

        [Column("Sinopsis")]
        [MaxLength(1000)]
        public string Sinopsis { get; set; }

        [Column("Duracion")]
        public int Duracion { get; set; } // Duración en minutos

        [Column("img")]
        [MaxLength(5000)]
        public string Img { get; set; }
    }
}
