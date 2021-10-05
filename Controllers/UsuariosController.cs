using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CitasMedicas.Models;
using CitasMedicas.Data;
using AutoMapper;
using CitasMedicas.Models.DTO;
using CitasMedicas.Services;

namespace CitasMedicas.Controllers
{
    [Route("usuarios")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly CitasMedicasContext _context;
        private readonly IMapper _mapper;
        private readonly IUsuarioService _usuarioService;

        public UsuariosController(CitasMedicasContext context, IMapper mapper, IUsuarioService usuarioService)
        {
            _context = context;
            _mapper = mapper;
            _usuarioService = usuarioService;
        }

        // GET: api/usuarios
        public ActionResult<IEnumerable<UsuarioDTO>> GetAllUsuarios()
        {
            IList<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();
            var usuarios = _usuarioService.ReadAllUsuarios();
            
            foreach (Usuario u in usuarios)
                usuariosDTO.Add(_mapper.Map<UsuarioDTO>(u));
            
            return Ok(new MessageDTO(200, usuariosDTO.ToList()));
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public ActionResult<UsuarioDTO> GetUsuario(int id)
        {
            var usuario = _usuarioService.ReadUsuario(id);

            if (usuario == null)
                return Ok(new MessageDTO(404, "El usuario con ID "+id+" no existe"));
            
            return Ok(new MessageDTO(200, _mapper.Map<UsuarioDTO>(usuario)));
        }

        // POST: api/usuarios
        [HttpPost]
        public ActionResult<Usuario> AddUsuario(Usuario usuario)
        {

            if (_usuarioService.CreateUsuario(usuario) == null)
                return Ok(new MessageDTO(404, "Ya existe un usuario con ID "+usuario.Id+" o NickUsuario "+ usuario.NickUsuario));
            
            return Ok(new MessageDTO(200, "Usuario con ID "+usuario.Id+" creado correctamente"));
             
        }

        // PUT: api/usuarios/5
        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, Usuario usuario)
        {
            if (_usuarioService.UpdateUsuario(id, usuario) == null)
                return Ok(new MessageDTO(404, "El usuario con ID "+usuario.Id+" no se encuentra o no se puede actualizar"));
            
            return Ok(new MessageDTO(200, "El usuario con ID "+usuario.Id+" se ha actualizado"));
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public ActionResult<Usuario> DeleteUsuario(int id)
        {
            if (_usuarioService.DeleteUsuario(id) != null)
                return Ok(new MessageDTO(200, "Usuario con ID "+id+" eliminado"));

            return  Ok(new MessageDTO(404, "El usuario con ID "+id+" no existe"));
                 
        }

    }
}
