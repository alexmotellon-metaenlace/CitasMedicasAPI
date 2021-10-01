using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CitasMedicas.Models;
using CitasMedicas.Data;
using AutoMapper;
using CitasMedicas.Models.DTO;
using CitasMedicas.Services;

namespace CiitasMedicas.Controllers
{
    [Route("diagnosticos")]
    [ApiController]
    public class DiagnosticosController : ControllerBase
    {
        private readonly CitasMedicasContext _context;
        private readonly IMapper _mapper;
        private readonly IDiagnosticoService _diagnosticoService;

        public DiagnosticosController(CitasMedicasContext context, IMapper mapper, IDiagnosticoService diagnosticoService)
        {
            _context = context;
            _mapper = mapper;
            _diagnosticoService = diagnosticoService;
        }

        // GET: api/diagnosticos
        [HttpGet]
        public ActionResult<IEnumerable<DiagnosticoDTO>> GetAllDiagnosticos()
        {
            IList<DiagnosticoDTO> diagnosticoDTO = new List<DiagnosticoDTO>();
            var diagnosticos = _diagnosticoService.ReadAllDiagnosticos();

            foreach (Diagnostico u in diagnosticos)
                diagnosticoDTO.Add(_mapper.Map<DiagnosticoDTO>(u));
            
            return Ok(new MessageDTO(200, diagnosticoDTO.ToList()));
        }

        // GET: api/diagnosticos/5
        [HttpGet("{id}")]
        public ActionResult<DiagnosticoDTO> GetDiagnostico(int id)
        {
            var diagnostico = _diagnosticoService.ReadDiagnostico(id);

            if (diagnostico == null)
                return Ok(new MessageDTO(404, "El diagnóstico con ID "+id+" no existe"));
            

            return Ok(new MessageDTO(200, _mapper.Map<DiagnosticoDTO>(diagnostico)));
        }

        // POST: api/diagnosticos
        [HttpPost]
        public ActionResult<Diagnostico> AddDiagnostico(Diagnostico diagnostico)
        {
            if (_diagnosticoService.CreateDiagnostico(diagnostico) == null)
                return Ok(new MessageDTO(404, "El diagnostico con ID "+diagnostico.Id+" ya existe o no se puede crear"));
            
            else
                return Ok(new MessageDTO(200, "Diagnostico con ID "+diagnostico.Id+" creado correctamente"));
            
        }

        // PUT: api/diagnosticos/5
        [HttpPut("{id}")]
        public IActionResult UpdateDiagnostico(int id, Diagnostico diagnostico)
        {
            if (_diagnosticoService.UpdateDiagnostico(id, diagnostico) == null)
                return Ok(new MessageDTO(404, "El diagnostico con ID "+id+" no se encuentra o no se puede actualizar"));
            
            return Ok(new MessageDTO(200, "Diagnostico con ID "+id+" actualizado"));
        }


        // DELETE: api/diagnosticos/5
        [HttpDelete("{id}")]
        public ActionResult<Diagnostico> DeleteDiagnostico(int id)
        {
            if (_diagnosticoService.DeleteDiagnostico(id) != null)
                return Ok(new MessageDTO(200, "Diagnostico con ID "+id+" eliminado correctamente"));
            
            else
               return Ok(new MessageDTO(400, "El diagnostico con ID "+id+" no se encuentra o no se puede eliminar "));     
        }
        
    }
}
