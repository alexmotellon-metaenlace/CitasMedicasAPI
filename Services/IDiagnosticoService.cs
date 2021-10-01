using CitasMedicas.Models;
using System.Collections.Generic;


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