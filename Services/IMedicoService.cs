using CitasMedicas.Models;
using CitasMedicas.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IMedicoService
    {
        public IEnumerable<Medico> ReadAllMedicos();
        public Medico ReadMedico(long id);
        public Medico CreateMedico(Medico medico);
        public Medico UpdateMedico(long id, Medico medico);
        public Medico DeleteMedico(long id);
        public Medico LoginMedico(string username, string password);
        public IEnumerable<Paciente> ReadPacientesMedico(long id);
        public IEnumerable<Cita> ReadCitasMedico(long id);
    }
}