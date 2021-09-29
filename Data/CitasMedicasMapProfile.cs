using AutoMapper;
using CitasMedicas.Models.DTO;
using CitasMedicas.Models;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CitasMedicas.Data
{
    public class CitasMedicasProfile : Profile
    {
        public CitasMedicasProfile()
        {
            CreateMap<Usuario, UsuarioDTO>();
            CreateMap<UsuarioDTO, Usuario>();

            CreateMap<Paciente, PacienteDTO>();
            CreateMap<PacienteDTO, Paciente>();
            
            CreateMap<Medico, MedicoDTO>();
            CreateMap<MedicoDTO, Medico>();

            CreateMap<DiagnosticoDTO, Diagnostico>();
            CreateMap<Diagnostico, DiagnosticoDTO>();


            CreateMap<Cita, CitaDTO>()
                .ForMember(cdto => cdto.FechaHora, o => o.MapFrom(cita => cita.FechaHora.ToString("dd-MM-yyyy HH:mm")))
                .ForMember(cdto => cdto.Medico, o => o.MapFrom(cita => cita.Medico.Id))
                .ForMember(cdto => cdto.Paciente, o => o.MapFrom(cita => cita.Paciente.Id))
                .ForMember(cdto => cdto.Diagnostico, o => o.MapFrom(cita => cita.Diagnostico.Id));

            CreateMap<CitaDTO, Cita>()
                .ForMember(cita => cita.FechaHora, o => o.MapFrom(dto => DateTime.ParseExact(dto.FechaHora, "dd-MM-yyyy HH:mm", CultureInfo.InvariantCulture)))
                .ForMember(cita => cita.Medico, o => o.Ignore())
                .ForMember(cita => cita.Paciente, o => o.Ignore())
                .ForMember(cita => cita.Diagnostico, o => o.Ignore());

        }
    }
}