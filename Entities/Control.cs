public class Control
{
    public bool swt { get; set; } = true;
    public int usuario_creacion { get; set; } = 1;
    public DateTime fecha_creacion { get; set; } = DateTime.Now;
    public int? usuario_modificacion { get; set; }
    public DateTime? fecha_modificacion { get; set; }
}