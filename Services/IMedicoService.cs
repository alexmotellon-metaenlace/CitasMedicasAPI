using CitasMedicas.Models;
using System.Collections.Generic;


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