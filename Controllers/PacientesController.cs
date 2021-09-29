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
    [Route("pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly CitasMedicasContext _context;
        private readonly IMapper _mapper;
        private readonly IPacienteService _pacienteService;

        public PacientesController(CitasMedicasContext context, IMapper mapper, IPacienteService pacienteService)
        {
            _context = context;
            _mapper = mapper;
            _pacienteService = pacienteService;
        }

        // GET: api/pacientes
        [HttpGet]
        public ActionResult<IEnumerable<PacienteDTO>> GetAllPacientes()
        {
            IList<PacienteDTO> pacientesDTO = new List<PacienteDTO>();
            var pacientes = _pacienteService.ReadAllPacientes();

            foreach (Paciente p in pacientes)
                pacientesDTO.Add(_mapper.Map<PacienteDTO>(p));
            
            return  Ok(new MessageDTO(200, pacientesDTO.ToList()));
        }

        // GET: api/pacientes/5
        [HttpGet("{id}")]
        public ActionResult<PacienteDTO> GetPaciente(int id)
        {
            var paciente = _pacienteService.ReadPaciente(id);

            if (paciente == null)            
                return Ok(new MessageDTO(404, "El paciente con ID "+id+" no existe"));

            return Ok(new MessageDTO(200,_mapper.Map<PacienteDTO>(paciente)));
        }

        // POST: api/pacientes
        [HttpPost]
        public ActionResult<Paciente> AddPaciente(Paciente paciente)
        {
            if (_pacienteService.CreatePaciente(paciente) == null)
                return Ok(new MessageDTO(404, "El paciente con ID "+paciente.Id+" ya existe"));
            
            else
                return Ok(new MessageDTO(200, "Paciente con ID "+paciente.Id+ " creado correctamente"));
            
        }

        // PUT: api/pacientes/5
        [HttpPut("{id}")]
        public IActionResult UpdatePaciente(int id, Paciente paciente)
        {
            if (_pacienteService.UpdatePaciente(id, paciente) == null)
                return Ok(new MessageDTO(404, "El paciente con ID "+paciente.Id+" no se encuentra o no se puede actualizar"));
            

            return Ok(new MessageDTO(200, "El paciente con ID "+paciente.Id+" se ha actualizado"));
        }


        // DELETE: api/pacientes/5
        [HttpDelete("{id}")]
        public ActionResult<Paciente> DeletePaciente(int id)
        {
            if (_pacienteService.DeletePaciente(id) != null)
                return Ok(new MessageDTO(200, "Paciente con ID "+id+" eliminado"));
            
            else
                return Ok(new MessageDTO(404, "El paciente con ID "+id+" no existe")); 
        }

        // POST: api/pacientes/Login
        [HttpPost("login")]
        public ActionResult<Paciente> Login(LoginDTO login)
        {
            Paciente pac = _pacienteService.LoginPaciente(login.NickUsuario, login.Clave);

            if (pac == null)
                return Ok(new MessageDTO(404, "El paciente con NickUsuario "+login.NickUsuario+" no existe")); 
            
            else
                return Ok(new MessageDTO(200, _mapper.Map<PacienteDTO>(pac)));
        }  

        // GET: api/pacientes/5/medicos
        [HttpGet("{id}/medicos")]
        public ActionResult<IEnumerable<MedicoDTO>> MedicosPaciente(long id)
        {
            IList<MedicoDTO> medicosDTO = new List<MedicoDTO>();
            var medicos = _pacienteService.ReadMedicosPaciente(id);

            if (medicos == null)
                return  Ok(new MessageDTO(404, "No se han encontrado medicos para el paciente con ID "+id));
            
            foreach (Medico m in medicos)
                medicosDTO.Add(_mapper.Map<MedicoDTO>(m));
            
            return  Ok(new MessageDTO(200, medicosDTO.ToList()));
        }

        // GET: api/pacientes/5/citas
        [HttpGet("{id}/citas")]
        public ActionResult<IEnumerable<CitaDTO>> CitasPaciente(long id)
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _pacienteService.ReadCitasPaciente(id);
            if (citas == null)
                return  Ok(new MessageDTO(404, "No se han encontrado citas para el paciente con ID "+id));
            
            foreach (Cita c in citas)
                citasDTO.Add(_mapper.Map<CitaDTO>(c));
            
            return  Ok(new MessageDTO(200, citasDTO.ToList()));
        }

    }
}
