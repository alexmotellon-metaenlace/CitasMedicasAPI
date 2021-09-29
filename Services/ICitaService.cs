using CitasMedicas.Models;
using CitasMedicas.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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