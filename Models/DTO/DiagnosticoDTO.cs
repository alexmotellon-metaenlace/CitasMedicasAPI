using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Models.DTO
{
    public class DiagnosticoDTO
    {
        public long Id { get; set; }
        public string ValoracionEspecialista { get; set; }
        public string Enfermedad { get; set; }
    }
}