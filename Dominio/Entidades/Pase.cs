﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Entidades
{
    public class Pase
    {
        public Guid PaseId { get; set; }

        //Atributos
        public DateTime FechaInicio { get; set; }

        public DateTime FechaTermino { get; set; }

        public string tipo { get; set; }

        public string Estado { get; set; }

        public string Motivo { get; set; }

        public string Area { get; set; }

        public enum EstadoPase
        {
            REVISION, ACTIVO, VENCIDO, RECHAZADO
        }

        // relacionamiento segun modelo R
        public Guid EmpresaId { get; set; }

        public Guid UsuarioId { get; set; }

        // obtener los modelos relacionados 
        public Empresa EmpresaRel { get; set; }

        public Usuario UsuarioRel { get; set; }

        public ICollection<Documento> DocumentosRel { get; set; }

        public ICollection<PasePersonaExterna> PersonasExternaRel { get; set; }

        public AsesorPrevencion Asesor { get; set; }


    }
}
