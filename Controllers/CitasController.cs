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
    [Route("citas")]
    [ApiController]
    public class CitasController : ControllerBase
    {
        private readonly CitasMedicasContext _context;
        private readonly IMapper _mapper;
        private readonly ICitaService _citaService;

        public CitasController(CitasMedicasContext context, IMapper mapper, ICitaService citaService)
        {
            _context = context;
            _mapper = mapper;
            _citaService = citaService;
        }

        // GET: api/citas
        [HttpGet]
        public ActionResult<IEnumerable<CitaDTO>> GetAllCitas()
        {
            IList<CitaDTO> citasDTO = new List<CitaDTO>();
            var citas = _citaService.ReadAllCitas();
            
            foreach (Cita u in citas)
                citasDTO.Add(_mapper.Map<CitaDTO>(u));
            
            return Ok(new MessageDTO(200, citasDTO.ToList()));
        }

        // GET: api/citas/5
        [HttpGet("{id}")]
        public ActionResult<CitaDTO> GetCita(int id)
        {
            var cita = _citaService.ReadCita(id);

            if (cita == null)
                return Ok(new MessageDTO(404, "La cita con ID "+id+" no existe"));
            

            return Ok(new MessageDTO(200, _mapper.Map<CitaDTO>(cita)));
        }

        // POST: api/citas
        [HttpPost]
        public ActionResult<Cita> AddCita(CitaDTO cita)
        {
            Cita cita_created = _citaService.CreateCita(_mapper.Map<Cita>(cita), cita.Medico, cita.Paciente);
            if (cita_created == null)
                return Ok(new MessageDTO(404, "La cita con ID "+cita_created.Id+" ya existe o no se puede crear"));
            
            else
                return Ok(new MessageDTO(200, "Cita con ID "+cita_created.Id+" creada correctamente"));
            
        }

        // PUT: api/citas/5
        [HttpPut("{id}")]
        public IActionResult UpdateCita(int id, Cita cita)
        {
            Cita cita_updated = _citaService.UpdateCita(id, cita);
            if (cita_updated == null)
                return Ok(new MessageDTO(404, "La cita con ID "+id+" no se encuentra o no se puede actualizar"));
            

            return Ok(new MessageDTO(200, "La cita con ID "+id+" se ha actualizado"));
        }



        // DELETE: api/citas/5
        [HttpDelete("{id}")]
        public ActionResult<Cita> DeleteCita(int id)
        {
            if (_citaService.DeleteCita(id) != null)
                return Ok(new MessageDTO(200, "Cita con ID "+id+" eliminada correctamente"));
            
            else
               return Ok(new MessageDTO(400, "La cita con ID "+id+" no se encuentra o no se puede eliminar "));
                 
        }

    }
}