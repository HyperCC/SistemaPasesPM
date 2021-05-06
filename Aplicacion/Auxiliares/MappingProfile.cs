using AutoMapper;
using Dominio.ModelosDto;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using System.Linq;

namespace Aplicacion.Auxiliares
{
    /// <summary>
    /// Mapeo de modelo relacionales con los DTO
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Usuario, UsuarioDto>()
                // relacion N-N
                .ForMember(x => x.PersonaDtoRel, y => y.MapFrom(z => z.PersonaRel))
                // relacion 1-N
                .ForMember(x => x.PasesDtoRel, y => y.MapFrom(z => z.PasesRel))
                .ForMember(x => x.EmpresaDtoRel, y => y.MapFrom(z => z.EmpresaRel));

            CreateMap<TipoNombre, TipoNombreDto>();
            CreateMap<PersonaTipoNombre, PersonaTipoNombreDto>();

            CreateMap<Persona, PersonaDto>()
                // obtener la lista de Personas para un TipoNombre -> RELACIONES N-N
                .ForMember(x => x.TipoNombresDtoRel, y => y.MapFrom(z => z.TipoNombresRel.Select(a => a.TipoNombreRel).ToList()));

        }
    }
}