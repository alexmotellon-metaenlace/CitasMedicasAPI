using CitasMedicas.Models;
using System.Collections.Generic;


namespace CitasMedicas.Services
{
    public interface ICitaService
    {
        public IEnumerable<Cita> ReadAllCitas();
        public Cita ReadCita(long id);
        public Cita CreateCita(Cita cita, long medico, long paciente);
        public Cita UpdateCita(long id, Cita cita);
        public Cita DeleteCita(long id);
    }
}