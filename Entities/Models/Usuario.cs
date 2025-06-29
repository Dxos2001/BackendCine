using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BackendCine.Entities.Models;

namespace Company.TestProject1;

[Table("Usuarios")]
public class Usuario : Control
{
    [Key]
    [Column("Id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("NomUsuario")]
    public string NomUsuario { get; set; }

    [Column("Password")]
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Column("Email")]
    public string Email { get; set; }

    [ForeignKey("Cliente")]
    public int IdCliente { get; set; }
    public virtual Cliente Cliente { get; set; }
}
