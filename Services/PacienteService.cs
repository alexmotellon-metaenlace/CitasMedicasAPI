using CitasMedicas.Models;
using CitasMedicas.Models.DTO;
using CitasMedicas.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public class PacienteService : IPacienteService
    {
        private CitasMedicasContext _context;

        public PacienteService(CitasMedicasContext context)
        {
            _context = context;
        }

        // GET: ReadAllPacientes 
        public IEnumerable<Paciente> ReadAllPacientes()
        {
            return _context.Pacientes.ToList();
        }

        // GET: ReadPaciente 
        public Paciente ReadPaciente(long id)
        {
            return _context.Pacientes.Where(p => p.Id == id).FirstOrDefault();
        }

        // POST: CreatePaciente 
        public Paciente CreatePaciente(Paciente paciente)
        {
            if (_context.Pacientes.Any(p => p.NickUsuario == paciente.NickUsuario) && _context.Pacientes.Any(p => p.Id == paciente.Id)
                && _context.Pacientes.Any(p => p.Nss == paciente.Nss))
                return null;

            _context.Pacientes.Add(paciente);
            _context.SaveChanges();

            return paciente;
        }

        // PUT: UpdatePaciente 
        public Paciente UpdatePaciente(long id, Paciente paciente)
        {
            if (id != paciente.Id)
                return null;

            _context.Entry(paciente).State = EntityState.Modified;
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // User does not exists
                if (! _context.Pacientes.Any(e => e.Id == id))
                    return null;
                else
                    throw;
            }
            return paciente;
        }

        // DELETE: DeletePaciente
        public Paciente DeletePaciente(long id)
        {
            var paciente = _context.Pacientes.Find(id);
            if (paciente == null)
                return null;
            
            _context.Pacientes.Remove(paciente);
            _context.SaveChanges();

            return paciente;
        }


        // POST: LoginPaciente
        public Paciente LoginPaciente(string username, string password)
        {
            if(_context.Pacientes.Any(e => e.NickUsuario == username && e.Clave == password))
                return _context.Pacientes.Where(e => e.NickUsuario == username).FirstOrDefault();
            
            return null;
        }


        // GET: ReadMedicosPaciente
        public IEnumerable<Medico> ReadMedicosPaciente(long id)
        {
            IList<Medico> medicos = new List<Medico>();
            var citas = ReadCitasPaciente(id);
            
            if (citas == null || citas.Count()<1)
                return null;
            
            foreach (Cita c in citas){
                Medico medico_aux = _context.Medicos.Find(c.Medico.Id);
                if (!medicos.Contains(medico_aux))
                    medicos.Add(_context.Medicos.Find(c.Medico.Id));
            }

            if (medicos.Count()<1)
                return null;

            return medicos;
        }

        // GET: ReadMedicosPaciente
        public IEnumerable<Cita> ReadCitasPaciente(long id)
        {
            var citas = _context.Citas
                .Where(c => c.Paciente.Id == id)
                .Include("Paciente")
                .Include("Medico")
                .ToList();

            if (citas.Count()<1)
                return null;
                
            return citas;
        }


    }
}