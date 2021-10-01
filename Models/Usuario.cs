using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CitasMedicas.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public long Id { get; set; }
        public string Clave { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string NickUsuario { get; set; }
    }
}