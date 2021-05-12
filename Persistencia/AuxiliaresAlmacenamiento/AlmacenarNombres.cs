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
        public static async Task AgregarNombres(string nombresContatenados, string apellidosContatenados, SistemaPasesContext _context, Guid _personaId)
        {

            // obtencion de nombres de la cadena bruta
            string[] nombres = nombresContatenados.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            // posicionamiento del nombre
            int currentIteration = 1;

            // asignacion de los nuevos nombres
            foreach (var nombre in nombres)
            {
                // buscar si existe el nombre
                var nombreExiste = await _context.TipoNombre
                    .Where(x => x.Nombre == nombre
                    && x.Tipo == TipoIdentificador.NOMBRE
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                var nuevoNombre = new TipoNombre();
                if (nombreExiste == null)
                {
                    nuevoNombre.TipoNombreId = new Guid();
                    nuevoNombre.Nombre = nombre;
                    nuevoNombre.Tipo = TipoIdentificador.NOMBRE;
                    nuevoNombre.Posicion = currentIteration;
                }
                _context.TipoNombre.Add(nuevoNombre);

                // agregar tabla con nombres y personas 
                var nuevoPersonaTipoNombre = new PersonaTipoNombre
                {
                    PersonaId = _personaId,
                    TipoNombreId = (nombreExiste == null) ?
                    nuevoNombre.TipoNombreId : nombreExiste.TipoNombreId
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
                var apellidoExiste = await _context.TipoNombre
                    .Where(x => x.Nombre == apellido
                    && x.Tipo == TipoIdentificador.APELLIDO
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                var nuevoApellido = new TipoNombre();
                if (apellidoExiste == null)
                {
                    nuevoApellido.TipoNombreId = new Guid();
                    nuevoApellido.Nombre = apellido;
                    nuevoApellido.Tipo = TipoIdentificador.APELLIDO;
                    nuevoApellido.Posicion = currentIteration;
                }
                _context.TipoNombre.Add(nuevoApellido);

                // agregar tabla con nombres y personas 
                var nuevoPersonaTipoNombre = new PersonaTipoNombre
                {
                    PersonaId = _personaId,
                    TipoNombreId = (apellidoExiste == null) ?
                    nuevoApellido.TipoNombreId : apellidoExiste.TipoNombreId
                };
                _context.PersonaTipoNombre.Add(nuevoPersonaTipoNombre);

                currentIteration++;
            }
        }
    }
}