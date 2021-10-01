using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace CitasMedicas.Models
{
    [Table("Diagnosticos")]
    public class Diagnostico
    {
        [Key]
        public long Id { get; set; }
        public string ValoracionEspecialista { get; set; }
        public string Enfermedad { get; set; }
    }
}