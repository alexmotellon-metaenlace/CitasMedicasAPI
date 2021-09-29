using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Models
{
    [Table("Diagnosticos")]
    public class Diagnostico
    {
        [Key][DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string ValoracionEspecialista { get; set; }
        public string Enfermedad { get; set; }
    }
}