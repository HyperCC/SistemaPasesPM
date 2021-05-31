﻿using Dominio.Entidades;
using Persistencia;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Dominio.ModelosDto;

namespace Aplicacion.Pases
{
    public class NuevoPaseVisita
    {
        ///<summary>
        ///Agregar una nueva solicitud Pase de Vistia
        ///</summary>
        
        public class Ejecuta : IRequest<Pase>
        {
            //Datos recibidos por formulario
            public DateTime fechaInicio { get; set; }
            public DateTime fechaTermino { get; set; }
            public string motivo { get; set; }
            public string area { get; set; }
            public string rutEmpresa { get; set; }
            public string nombreEmpresa { get; set; }
            public ICollection<PersonaPaseVisita> listadoPersonas { get; set; }

        }
        public class Manejador : IRequestHandler<Ejecuta, Pase>
        {
            private readonly SistemaPasesContext _context;

            public Manejador(SistemaPasesContext context)
            {
                this._context = context;
            }

            public async Task<Pase> Handle(Ejecuta request, CancellationToken cancellationToken)
            {
                //PASE
                var solicitudPaseNueva = new Pase
                {
                    PaseId = new Guid(),
                    FechaInicio = request.fechaInicio,
                    FechaTermino = request.fechaTermino,
                    Tipo = TipoPase.VISITA,
                    Estado = EstadoPase.PENDIENTE,
                    Motivo = request.motivo,
                    Area = request.area
                };

                //EMPRESA
                // Buscar si la Empresa esta ya registrada
                var empresaExiste = await this._context.Empresa.Where(x => x.Rut == request.rutEmpresa).FirstOrDefaultAsync();

                // Si no existe la empresa se crea una nueva
                if (empresaExiste == null)
                {
                    var empresaGenerada = new Empresa
                    {
                        EmpresaId = new Guid(),
                        Rut = request.rutEmpresa,
                        Nombre = request.nombreEmpresa
                    };

                    // agregar la nueva empresa
                    await this._context.Empresa.AddAsync(empresaGenerada);
                    // relacionar empresa con Pase
                    solicitudPaseNueva.EmpresaId = empresaGenerada.EmpresaId;
                }
                else
                {
                    solicitudPaseNueva.EmpresaId = empresaExiste.EmpresaId;
                }

                await this._context.Pase.AddAsync(solicitudPaseNueva);

                //LISTADO DE PERSONAS
                if (request.listadoPersonas != null)
                {
                    foreach (var persona in request.listadoPersonas)
                    {
                        //Debido a que el pase siempre sera nuevo la relacion Pase->PersonaExternas siempre sera unico
                        //por lo que no es necesario verificar la existencia
                        var nuevoPasePersona = new PasePersonaExterna
                        {
                            PaseId = solicitudPaseNueva.PaseId
                        };

                        

                        //Buscamos si la persona ya esta registrada por su rut
                        var personaExiste = await this._context.Persona.Where(x => x.Rut == persona.rut).FirstOrDefaultAsync();

                        //Creamos una nueva persona si esta no existe en la BD
                        if (personaExiste == null)
                        {
                            var personaNueva = new Persona
                            {
                                PersonaId = new Guid(),
                                Rut = persona.rut
                            };
                            await this._context.Persona.AddAsync(personaNueva);

                            string[] nombresPersonaNueva = persona.nombres.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                            int iteracion = 1;
                            foreach (var nombre in nombresPersonaNueva)
                            {
                                //Objeto para relacionar la persona con el nombre
                                var nuevoPersonaTipoNombre = new PersonaTipoNombre();

                                // Buscar si existe el nombre en la BD
                                var nombreExiste = await this._context.TipoNombre
                                    .Where(x => x.Nombre == nombre
                                    && x.Tipo == TipoIdentificador.NOMBRE
                                    && x.Posicion == iteracion)
                                    .FirstOrDefaultAsync();
                                // Agregar el nuevo nombre si no existe en la BD
                                if (nombreExiste == null)
                                {
                                    var nombreNuevo = new TipoNombre
                                    {
                                        TipoNombreId = new Guid(),
                                        Nombre = nombre,
                                        Tipo = TipoIdentificador.NOMBRE,
                                        Posicion = iteracion
                                    };
                                    // 
                                    this._context.TipoNombre.Add(nombreNuevo);

                                    // Vinculamos el nuevo nombre con la persona correspondiente
                                    nuevoPersonaTipoNombre.TipoNombreId = nombreNuevo.TipoNombreId;
                                }
                                else
                                {
                                    nuevoPersonaTipoNombre.TipoNombreId = nombreExiste.TipoNombreId;
                                }
                                //Vinculamos el nuevo nombre con la persona correspondiente
                                nuevoPersonaTipoNombre.PersonaId = personaNueva.PersonaId;

                                iteracion++;
                            }

                            //APELLIDO PATERNO
                            var nuevoPersonaTipoApellido = new PersonaTipoNombre
                            {
                                PersonaId = personaNueva.PersonaId
                            };

                            var apellidoExiste = await this._context.TipoNombre
                                .Where(x => x.Nombre == persona.apellidoPaterno
                                && x.Tipo == TipoIdentificador.APELLIDO
                                && x.Posicion == 1)
                                .FirstOrDefaultAsync();

                            if (apellidoExiste == null)
                            {
                                var nuevoApellido = new TipoNombre
                                {
                                    TipoNombreId = new Guid(),
                                    Nombre = persona.apellidoPaterno,
                                    Tipo = TipoIdentificador.APELLIDO,
                                    Posicion = 1
                                };

                                this._context.TipoNombre.Add(nuevoApellido);

                                nuevoPersonaTipoApellido.TipoNombreId = nuevoApellido.TipoNombreId;
                            }
                            else
                            {
                                nuevoPersonaTipoApellido.TipoNombreId = apellidoExiste.TipoNombreId;
                            }

                            this._context.PersonaTipoNombre.Add(nuevoPersonaTipoApellido);

                            //APELLIDO MATERNO
                            nuevoPersonaTipoApellido = new PersonaTipoNombre
                            {
                                PersonaId = personaNueva.PersonaId
                            };

                            apellidoExiste = await this._context.TipoNombre
                                .Where(x => x.Nombre == persona.apellidoMaterno
                                && x.Tipo == TipoIdentificador.APELLIDO
                                && x.Posicion == 2)
                                .FirstOrDefaultAsync();

                            if (apellidoExiste == null)
                            {
                                var nuevoApellido = new TipoNombre
                                {
                                    TipoNombreId = new Guid(),
                                    Nombre = persona.apellidoMaterno,
                                    Tipo = TipoIdentificador.APELLIDO,
                                    Posicion = 2
                                };

                                this._context.TipoNombre.Add(nuevoApellido);

                                nuevoPersonaTipoApellido.TipoNombreId = nuevoApellido.TipoNombreId;
                            }
                            else
                            {
                                nuevoPersonaTipoApellido.TipoNombreId = apellidoExiste.TipoNombreId;
                            }

                            this._context.PersonaTipoNombre.Add(nuevoPersonaTipoApellido);

                            //Generamos la instancia de Persona Externa
                            var nuevaPersonaExterna = new PersonaExterna
                            {
                                PersonaExternaId = new Guid(),
                                Nacionalidad = persona.nacionalidad,
                                //TODO Confirmar correspondencia del pasaporte
                                Pasaporte = (persona.nacionalidad != "CHILENA") ? persona.rut : null
                            };

                            //Vinculamos la persona con PersonaExterna
                            nuevaPersonaExterna.PersonaId = personaNueva.PersonaId;

                            await this._context.PersonaExterna.AddAsync(nuevaPersonaExterna);

                            // Vinculamos con PasePersonaExterna
                            nuevoPasePersona.PersonaExternaId = nuevaPersonaExterna.PersonaExternaId;

                            this._context.PasePersonaExterna.Add(nuevoPasePersona);
                        }
                        else
                        {
                            // Buscamos si existe persona Externa vinculada al rut
                            var personaExternaExiste = await this._context.PersonaExterna
                                                    .Where(x => x.PersonaId == personaExiste.PersonaId)
                                                    .FirstOrDefaultAsync();

                            if (personaExternaExiste == null)
                            {
                                var nuevaPersonaExterna = new PersonaExterna
                                {
                                    PersonaExternaId = new Guid(),
                                    Nacionalidad = persona.nacionalidad,
                                    //TODO Confirmar correspondencia del pasaporte
                                    Pasaporte = (persona.nacionalidad != "CHILENA") ? persona.rut : null
                                };

                                //Vinculamos la persona con PersonaExterna
                                nuevaPersonaExterna.PersonaId = personaExiste.PersonaId;

                                await this._context.PersonaExterna.AddAsync(nuevaPersonaExterna);

                                // Vinculamos con PasePersonaExterna
                                nuevoPasePersona.PersonaExternaId = nuevaPersonaExterna.PersonaExternaId;

                                this._context.PasePersonaExterna.Add(nuevoPasePersona);
                            }
                            else
                            {
                                nuevoPasePersona.PersonaExternaId = personaExternaExiste.PersonaExternaId;
                            }                           
                            
                        }
                                                
                        this._context.PasePersonaExterna.Add(nuevoPasePersona);
                    }
                }                

                var resultado = await this._context.SaveChangesAsync();
                if (resultado > 0)
                {
                    // agregar devolucion de usuario DTO
                    return solicitudPaseNueva;
                }

                throw new Exception("No se pudo agregar el nuevo pase");

                throw new NotImplementedException();
            }
        }
    }
}
