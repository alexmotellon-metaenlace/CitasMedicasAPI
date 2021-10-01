using System.ComponentModel.DataAnnotations.Schema;


namespace CitasMedicas.Models
{
    [Table("Pacientes")]
    public class Paciente : Usuario 
    {
        public string Nss { get; set; }
        public string NumTarjeta { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

    }
}