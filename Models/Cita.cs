using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Models
{
    [Table("Citas")]
    public class Cita
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string MotivoCita { get; set; }
        public DateTime FechaHora { get; set; }
        public int Attribute11 { get; set; }
        public Diagnostico Diagnostico { get; set; }
        public Medico Medico { get; set; }
        public Paciente Paciente { get; set; }
    }
}