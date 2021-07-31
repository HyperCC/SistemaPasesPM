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
                Nombre nombreExiste = await _context.Nombre
                    .Where(x => x.Titulo == nombre
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                if (nombreExiste == null)
                {
                    nombreExiste = new Nombre
                    {
                        NombreId = new Guid(),
                        Titulo = nombre,
                        Posicion = currentIteration
                    };
                    _context.Nombre.Add(nombreExiste);
                };

                // agregar tabla con nombres y personas 
                NombrePersona nuevoPersonaTipoNombre = new NombrePersona
                {
                    PersonaId = _personaId,
                    NombreId = nombreExiste.NombreId
                };
                _context.NombrePersona.Add(nuevoPersonaTipoNombre);

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
                Apellido apellidoExiste = await _context.Apellido
                    .Where(x => x.Titulo == apellido
                    && x.Posicion == currentIteration)
                    .FirstOrDefaultAsync();

                // agregar el nuevo nombre si no existe
                if (apellidoExiste == null)
                {
                    apellidoExiste = new Apellido
                    {
                        ApellidoId = new Guid(),
                        Titulo = apellido,
                        Posicion = currentIteration
                    };
                    _context.Apellido.Add(apellidoExiste);
                }

                // agregar tabla con nombres y personas 
                ApellidoPersona nuevoPersonaTipoNombre = new ApellidoPersona
                {
                    PersonaId = _personaId,
                    ApellidoId = apellidoExiste.ApellidoId
                };
                _context.ApellidoPersona.Add(nuevoPersonaTipoNombre);

                currentIteration++;
            }
        }
    }
}