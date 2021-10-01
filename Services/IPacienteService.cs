using CitasMedicas.Models;
using System.Collections.Generic;


namespace CitasMedicas.Services
{
    public interface IPacienteService
    {
        public IEnumerable<Paciente> ReadAllPacientes();
        public Paciente ReadPaciente(long id);
        public Paciente CreatePaciente(Paciente paciente);
        public Paciente UpdatePaciente(long id, Paciente paciente);
        public Paciente DeletePaciente(long id);
        public Paciente LoginPaciente(string username, string password);
        public IEnumerable<Medico> ReadMedicosPaciente(long id);
        public IEnumerable<Cita> ReadCitasPaciente(long id);
    }
}