using System;
using System.Collections.Generic;
using System.Text;

namespace Dominio.Auxiliares.Pases
{
    enum SeleccionarPasaporteORut
    {
        RUT, PASAPORTE
    }

    /// <summary>
    /// Clase para recibir los datos desde el cliente por cada persona externa
    /// </summary>
    public class PersonaExternaGenericaRequest
    {
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string PasaporteORut { get; set; }
        public string Pasaporte { get; set; }
        public string Rut { get; set; }
        public string Nacionalidad { get; set; }
    }
}