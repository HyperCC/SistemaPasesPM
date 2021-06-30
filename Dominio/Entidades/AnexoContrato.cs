using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class AnexoContrato
    {
        public Guid AnexoContratoId { get; set; }

        // atributos de la entidad
        public string Descripcion { get; set; }

        // relacionamiento segun modelo R
        public Guid DocumentoId { get; set; }

        // obtener los modelos relacionados 
        public Documento DocumentoRel { get; set; }
    }
}