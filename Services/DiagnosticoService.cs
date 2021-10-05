using CitasMedicas.Models;
using CitasMedicas.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


namespace CitasMedicas.Services
{
    public class DiagnosticoService : IDiagnosticoService
    {
        private CitasMedicasContext _context;

        public DiagnosticoService(CitasMedicasContext context)
        {
            _context = context;
        }
        
        // GET: ReadAllDiagnosticos 
        public IEnumerable<Diagnostico> ReadAllDiagnosticos()
        {
            return _context.Diagnosticos.ToList();
        }

        // GET: ReadDiagnostico 
        public Diagnostico ReadDiagnostico(long id)
        {
            return _context.Diagnosticos.Find(id);
        }

        // POST: CreateDiagnostico 
        public Diagnostico CreateDiagnostico(Diagnostico diagnostico)
        {
            if (_context.Diagnosticos.Any(d => d.Id == diagnostico.Id))
                return null;

            Cita cita = _context.Citas.Find(diagnostico.Id);
                        
            _context.Diagnosticos.Add(diagnostico);
            _context.SaveChanges();

            return diagnostico;
        }

        // PUT: UpdateDiagnostico 
        public Diagnostico UpdateDiagnostico(long id, Diagnostico diagnostico)
        {
            if (id != diagnostico.Id)
                return null;
            

            _context.Entry(diagnostico).State = EntityState.Modified;
            
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // Diagnostico does not exists
                if (! _context.Diagnosticos.Any(d => d.Id == id))
                    return null;
                
                else
                    throw;
                
            }
            return diagnostico;
        }

        // DELETE: DeleteDiagnostico
        public Diagnostico DeleteDiagnostico(long id)
        {
            var diagnostico = _context.Diagnosticos.Find(id);
            if (diagnostico == null)
                return null;
            
            _context.Diagnosticos.Remove(diagnostico);
            _context.SaveChanges();

            return diagnostico;
        }


    }
}