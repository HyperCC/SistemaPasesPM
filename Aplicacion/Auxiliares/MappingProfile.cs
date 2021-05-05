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
            CreateMap<TipoNombre, TipoNombreDto>()
                // obtener la lista de Personas para un TipoNombre -> RELACIONES N-N
                .ForMember(x => x.Personas, y => y.MapFrom(z => z.PersonasRel.Select(a => a.PersonaRel).ToList()));
            
            CreateMap<PersonaTipoNombre, PersonaTipoNombreDto>();
            CreateMap<Persona, PersonaDto>();
        }
    }
}
