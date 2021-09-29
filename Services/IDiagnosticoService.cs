using CitasMedicas.Models;
using CitasMedicas.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IDiagnosticoService
    {
        public IEnumerable<Diagnostico> ReadAllDiagnosticos();

        public Diagnostico ReadDiagnostico(long id);

        public Diagnostico CreateDiagnostico(Diagnostico diagnostico);
        
        public Diagnostico UpdateDiagnostico(long id, Diagnostico diagnostico);

        public Diagnostico DeleteDiagnostico(long id);
    }
}