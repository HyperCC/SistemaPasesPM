using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// clase auxiliar para almacenar los nombres y apellidos por persona
    /// </summary>
    public static class AlmacenarNombres
    {
        /// <summary>
        /// agregar los nombres recibidos a la base de datos
        /// </summary>
        /// <param name="nombres"></param>
        /// <param name="apellidos"></param>
        /// <param name="_context"></param>
        public static async Task AgregarNombres(string nombresContatenados,
            string apellidosContatenados,
            SistemaPasesContext _context,
            Guid _personaId)
        {

            // obtencion de nombres de la cadena bruta
            string[] nombres = nombresContatenados.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // posicionamiento del nombre
            int currentIteration = 1;

            // asignacion de los nuevos nombres
            foreach (var nombre in nombres)
            {
                // buscar si existe el nombre
                TipoNombre nombreExiste = await _context.TipoNombre
                    .Where(x => x.Nombre == nombre
                    && x.Tipo == TipoIdentificador.NOMBRE
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                if (nombreExiste == null)
                {
                    nombreExiste = new TipoNombre
                    {
                        TipoNombreId = new Guid(),
                        Nombre = nombre,
                        Tipo = TipoIdentificador.NOMBRE,
                        Posicion = currentIteration
                    };
                    _context.TipoNombre.Add(nombreExiste);
                };

                // agregar tabla con nombres y personas 
                PersonaTipoNombre nuevoPersonaTipoNombre = new PersonaTipoNombre
                {
                    PersonaId = _personaId,
                    TipoNombreId = nombreExiste.TipoNombreId
                };
                _context.PersonaTipoNombre.Add(nuevoPersonaTipoNombre);

                currentIteration++;
            }


            // obtencion de apellidos de la cadena bruta
            string[] apellidos = apellidosContatenados.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // posicionamiento del apellido
            currentIteration = 1;

            // asignacion de los nuevos apellidos
            foreach (var apellido in apellidos)
            {
                // buscar si existe el apellido
                TipoNombre apellidoExiste = await _context.TipoNombre
                    .Where(x => x.Nombre == apellido
                    && x.Tipo == TipoIdentificador.APELLIDO
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                if (apellidoExiste == null)
                {
                    apellidoExiste = new TipoNombre
                    {
                        TipoNombreId = new Guid(),
                        Nombre = apellido,
                        Tipo = TipoIdentificador.APELLIDO,
                        Posicion = currentIteration
                    };
                    _context.TipoNombre.Add(apellidoExiste);
                }

                // agregar tabla con nombres y personas 
                PersonaTipoNombre nuevoPersonaTipoNombre = new PersonaTipoNombre
                {
                    PersonaId = _personaId,
                    TipoNombreId = apellidoExiste.TipoNombreId
                };
                _context.PersonaTipoNombre.Add(nuevoPersonaTipoNombre);

                currentIteration++;
            }
        }
    }
}