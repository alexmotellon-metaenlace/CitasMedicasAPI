using CitasMedicas.Models;
using CitasMedicas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CitasMedicas.Services
{
    public class CitaService : ICitaService
    {
        private CitasMedicasContext _context;

        public CitaService (CitasMedicasContext context)
        {
            _context = context;
        }

        // GET: ReadAllCitas
        public IEnumerable<Cita> ReadAllCitas()
        {
            return _context.Citas.Include(c => c.Medico).Include(c => c.Paciente).ToList();
        }

        // GET: ReadCita
        public Cita ReadCita(long id)
        {
            return _context.Citas.Where(c => c.Id == id).Include(c => c.Medico).Include(c => c.Paciente).FirstOrDefault();
        }

        // POST: CreateCita
        public Cita CreateCita(Cita cita, long idmedico, long idpaciente)
        {
            if (cita == null)
                return null;
            

            Medico med = _context.Medicos.Find(idmedico);
            Paciente pac = _context.Pacientes.Find(idpaciente);
            
            if (med == null || pac == null)
                return null;
    
            cita.Medico = med;
            cita.Paciente = pac;
            
            _context.Citas.Add(cita);
            _context.SaveChanges();

            return cita;
        }

        // PUT: UpdateCita 
        public Cita UpdateCita(long id, Cita cita)
        {
            if (id != cita.Id)
                return null;
            

            _context.Entry(cita).State = EntityState.Modified;
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Cita does not exists
                if (! _context.Citas.Any(e => e.Id == id))
                    return null;
                
                else
                    throw;
                
            }
            return cita;
        }
        
        // DELETE: DeleteCita
        public Cita DeleteCita(long id)
        {
            var cita = _context.Citas.Find(id);
            if (cita == null)
                return null;
            

            _context.Citas.Remove(cita);
            _context.SaveChanges();

            return cita;
        }

    }
}