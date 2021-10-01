using System.ComponentModel.DataAnnotations.Schema;


namespace CitasMedicas.Models
{
    [Table("Medicos")]
    public class Medico : Usuario
    {
        public string NumColegiado { get; set; }

    }
}