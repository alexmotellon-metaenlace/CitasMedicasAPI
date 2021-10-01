using CitasMedicas.Models;
using CitasMedicas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CitasMedicas.Services
{
    public class MedicoService : IMedicoService
    {
        private CitasMedicasContext _context;

        public MedicoService(CitasMedicasContext context)
        {
            _context = context;
        }

        // GET: ReadAllMedicos
        public IEnumerable<Medico> ReadAllMedicos()
        {
            return _context.Medicos.ToList();
        }

        // GET: ReadMedico
        public Medico ReadMedico(long id)
        {
            return _context.Medicos.Find(id);
        }

        // POST: CreateMedico 
        public Medico CreateMedico(Medico medico)
        {
            if (_context.Medicos.Any(p => p.NickUsuario == medico.NickUsuario) && _context.Medicos.Any(p => p.Id == medico.Id)
                && _context.Medicos.Any(p => p.NumColegiado == medico.NumColegiado))
                return null;
                
            _context.Medicos.Add(medico);
            _context.SaveChanges();

            return medico;
        }

        // PUT: UpdateMedico 
        public Medico UpdateMedico(long id, Medico medico)
        {
            if (id != medico.Id)
                return null;
            

            _context.Entry(medico).State = EntityState.Modified;
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Medico does not exists
                if (! _context.Medicos.Any(e => e.Id == id))
                    return null;
                
                else
                    throw;
                
            }
            return medico;
        }

        // DELETE: DeleteMedico
        public Medico DeleteMedico(long id)
        {
            var medico = _context.Medicos.Find(id);
            if (medico == null)
                return null;
            

            _context.Medicos.Remove(medico);
            _context.SaveChanges();

            return medico;
        }

        // POST: LoginMedico
        public Medico LoginMedico(string username, string password)
        {
            if (_context.Medicos.Any(e => e.NickUsuario == username && e.Clave == password))
                return _context.Medicos.Where(e => e.NickUsuario == username).FirstOrDefault();
            
            return null;
        }

        // GET: ReadPacientesMedico
        public IEnumerable<Paciente> ReadPacientesMedico(long id)
        {
            IList<Paciente> pacientes = new List<Paciente>();
            var citas = ReadCitasMedico(id);

            if (citas.Count()<1)
                return null;
            
            foreach (Cita c in citas){
                Paciente paciente_aux = _context.Pacientes.Find(c.Paciente.Id);
                if (!pacientes.Contains(paciente_aux))
                    pacientes.Add(_context.Pacientes.Find(c.Paciente.Id));
            }

            if (pacientes.Count()<1)
                return null;
            
            return pacientes;
        }

        // GET: ReadCitasMedico 
        public IEnumerable<Cita> ReadCitasMedico(long id)
        {
            var citas = _context.Citas
                .Where(c => c.Medico.Id == id)
                .Include("Paciente")
                .Include("Medico")
                .ToList();

            if (citas.Count()<1)
                return null;
            
            return citas;
        }


    }
}