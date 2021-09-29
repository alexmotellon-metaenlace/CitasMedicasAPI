using CitasMedicas.Models.DTO;
using CitasMedicas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Services
{
    public interface IUsuarioService
    {
        public IEnumerable<Usuario> ReadAllUsuarios();
        public Usuario ReadUsuario(long id);
        public Usuario CreateUsuario(Usuario usuario);
        public Usuario UpdateUsuario(long id, Usuario usuario);
        public Usuario DeleteUsuario(long id);
    }
}