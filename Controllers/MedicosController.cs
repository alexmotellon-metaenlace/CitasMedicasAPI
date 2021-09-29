using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CitasMedicas.Models;
using CitasMedicas.Data;
using AutoMapper;
using CitasMedicas.Models.DTO;
using CitasMedicas.Services;


namespace CitasMedicas.Controllers
{
    [Route("medicos")]
    [ApiController]
    public class MedicosController : ControllerBase
    {
        private readonly CitasMedicasContext _context;
        private readonly IMapper _mapper;
        private readonly IMedicoService _medicoService;

        public MedicosController(CitasMedicasContext context, IMapper mapper, IMedicoService medicoService)
        {
            _context = context;
            _mapper = mapper;
            _medicoService = medicoService;
        }

        // GET: api/medicos
        [HttpGet]
        public ActionResult<IEnumerable<MedicoDTO>> GetAllMedicos()
        {
            IList<MedicoDTO> medicosDTO = new List<MedicoDTO>();
            var medicos = _medicoService.ReadAllMedicos();

            foreach (Medico m in medicos)
                medicosDTO.Add(_mapper.Map<MedicoDTO>(m));
            
            return Ok(new MessageDTO(200, medicosDTO));
        }

        // GET: api/medicos/5
        [HttpGet("{id}")]
        public ActionResult<MedicoDTO> GetMedico(int id)
        {
            Medico medico = _medicoService.ReadMedico(id);

            if (medico == null)
                return Ok(new MessageDTO(404, "El médico con ID "+id+" no existe"));
            

           return Ok(new MessageDTO(200, _mapper.Map<MedicoDTO>(medico)));
        }

        // POST: api/medicos
        [HttpPost]
        public ActionResult<Medico> AddMedico(Medico medico)
        {

            if (_medicoService.CreateMedico(medico) == null)
                return Ok(new MessageDTO(412, "El médico con ID "+medico.Id+" ya existe"));
            
            else
                return Ok(new MessageDTO(200, "Médico con ID "+medico.Id+" creado correctamente"));
            
        }

        // PUT: api/medicos/5
        [HttpPut("{id}")]
        public IActionResult PutMedico(int id, Medico medico)
        {
            if (_medicoService.UpdateMedico(id, medico) == null)
                return Ok(new MessageDTO(404, "El medico con ID "+id+" no se encuentra o no se puede actualizar"));

            return Ok(new MessageDTO(200, "El medico con ID "+id+" se ha actualizado"));
        }

        // DELETE: api/medicos/5
        [HttpDelete("{id}")]
        public ActionResult<Medico> DeleteMedico(int id)
        {
            if (_medicoService.DeleteMedico(id) != null)
                return Ok(new MessageDTO(200, "Médico con ID "+id+" eliminado correctamente"));
            
            else
                return Ok(new MessageDTO(404, "El médico con ID "+id+" no existe"));
               
        }

        // POST: api/medicos/Login
        [HttpPost("login")]
        public ActionResult<Medico> LoginMedico(LoginDTO login)
        {
            Medico med = _medicoService.LoginMedico(login.NickUsuario, login.Clave);

            if (med == null)
                return Ok(new MessageDTO(404, "El médico con NickUsuario "+login.NickUsuario+" no existe"));
            
            else
                return Ok(new MessageDTO(200, _mapper.Map<MedicoDTO>(med)));
            
        }

        // GET: api/medicos/5/pacientes
        [HttpGet("{id}/pacientes")]
        public ActionResult<IEnumerable<PacienteDTO>> PacientesMedico(long id)
        {
            IList<PacienteDTO> pacientesDTO = new List<PacienteDTO>();
            var pacientes = _medicoService.ReadPacientesMedico(id);

            if (pacientes == null) 
                return Ok(new MessageDTO(404, "No se han encontrado pacientes asociados al médico con ID "+id));
            
            foreach (Paciente p in pacientes)
                pacientesDTO.Add(_mapper.Map<PacienteDTO>(p));
            
            return Ok(new MessageDTO(200, pacientesDTO.ToList()));
        }

        // GET: api/medicos/5/citas
        [HttpGet("{id}/citas")]
        public ActionResult<IEnumerable<CitaDTO>> CitasMedico(long id)
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _medicoService.ReadCitasMedico(id);
            
            if (citas == null) 
                return Ok(new MessageDTO(404, "No se han encontrado citas asociadas al médico con ID "+id));
            
            foreach (Cita c in citas)
                citasDTO.Add(_mapper.Map<CitaDTO>(c));
            
            return  Ok(new MessageDTO(200, citasDTO.ToList()));
        }
        
    }
}
