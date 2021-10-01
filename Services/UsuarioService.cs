using CitasMedicas.Models;
using CitasMedicas.Data;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace CitasMedicas.Services
{
    public class UsuarioService : IUsuarioService
    {
        private CitasMedicasContext _context;

        public UsuarioService(CitasMedicasContext context)
        {
            _context = context;
        }
        // GET: ReadAllUsuarios
        public IEnumerable<Usuario> ReadAllUsuarios()
        {
            return _context.Usuarios.ToList(); 
        }

        // GET: ReadUsuario
        public Usuario ReadUsuario(long id)
        {
            return _context.Usuarios.Find(id);
        }

        // POST: CreateUsuario
        public Usuario CreateUsuario(Usuario usuario)
        {
 
            if (_context.Usuarios.Any(e => e.NickUsuario == usuario.NickUsuario))
                return null;

            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            return usuario;
        }

        // PUT: UpdateUsuario
        public Usuario UpdateUsuario(long id, Usuario usuario)
        {
            if (id != usuario.Id)            
                return null;
            

            _context.Entry(usuario).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                // User does not exists
                if (! _context.Usuarios.Any(e => e.Id == id))
                    return null;
                
                else
                    throw;
                
            }
            return usuario;
        }

        // DELETE: DeleteUsuario
        public Usuario DeleteUsuario(long id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
                return null;

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();

            return usuario;
        }
    }
}