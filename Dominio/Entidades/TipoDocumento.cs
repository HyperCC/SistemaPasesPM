﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class TipoDocumento
    {
        public Guid TipoDocumentoId { get; set; }

        // atributos de la entidad
        public string Titulo { get; set; }
        public bool Obligatoriedad { get; set; }
        public bool IsUnique { get; set; }

        // obtener los modelos relacionados 
        public ICollection<Documento> DocumentosRel { get; set; }
    }
}