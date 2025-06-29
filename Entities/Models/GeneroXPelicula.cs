using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackendCine.Entities.Models
{
    [Table("GenerosXPeliculas")]
    public class GeneroXPelicula : Control
    {
        [Key]
        [Column("Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("Genero")]
        public int IdGenero { get; set; }
        public Genero Genero { get; set; }

        [ForeignKey("Pelicula")]
        public int IdPelicula { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}