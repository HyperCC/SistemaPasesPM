using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Empresa
    {
        public Guid EmpresaId { get; set; }

        // atributos de la entidad
        public string Nombre { get; set; }
        public string Rut { get; set; }

        // obtener los modelos relacionados 
        public ICollection<Usuario> UsuariosRel { get; set; }
        public ICollection<Pase> PasesRel { get; set; }
        public ICollection<Documento> DocumentosRel { get; set; }
    }
}