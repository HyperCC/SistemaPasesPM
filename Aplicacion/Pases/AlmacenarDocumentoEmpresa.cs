using Dominio.Auxiliares.Pases;
using Dominio.Entidades;
using FluentValidation;
using Aplicacion.ExcepcionesPersonalizadas;
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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Aplicacion.Pases
{
    public class AlmacenarDocumentoEmpresa
    {
        public class Ejecuta : IRequest<Documento>
        {
            public IFormFile Documento { get; set; }
        }

        public class EjecutaValidacion : AbstractValidator<Ejecuta>
        {
            public EjecutaValidacion()
            {
                RuleFor(x => x.Documento).NotEmpty();
            }
        }

        public class Manejador : IRequestHandler<Ejecuta, Documento>
        {
            private readonly SistemaPasesContext _context;
            private readonly IHostingEnvironment _env;
            public Manejador(SistemaPasesContext context, IHostingEnvironment env)
            {
                this._context = context;
                this._env = env;
            }

            public async Task<Documento> Handle(Ejecuta request, CancellationToken cancellationToken)
            {

                Documento documentoNuevo = new Documento {
                    DocumentoId =  Guid.NewGuid()
                };

                string[] permittedExtensions = { ".txt", ".pdf" };
                
                var extensionArchivo = Path.GetExtension(request.Documento.FileName).ToLowerInvariant();

                if (string.IsNullOrEmpty(extensionArchivo) || !permittedExtensions.Contains(extensionArchivo))
                {
                    //Throw Exception
                    return new Documento
                    {
                        DocumentoId = Guid.NewGuid(),
                        RutaDocumento = "Aqui/No"
                    };
                }
                else
                {
                    var wwwRoute = _env.WebRootPath;
                    
                    var ruta = Path.Combine(wwwRoute, "Uploads");

                    if (!Directory.Exists(ruta))
                    {
                        Directory.CreateDirectory(ruta);
                    }
                    var docName = Path.GetFileName(request.Documento.FileName);
                    var rutaArchivo = Path.Combine(ruta, documentoNuevo.DocumentoId.ToString() + extensionArchivo);

                    documentoNuevo.RutaDocumento = rutaArchivo;

                    using (var stream = new FileStream(documentoNuevo.RutaDocumento, FileMode.Create))
                    {
                        await request.Documento.CopyToAsync(stream);
                        
                    }

                    this._context.Documento.Add(documentoNuevo);

                    var resultado = await this._context.SaveChangesAsync();

                    if (resultado > 0)
                    {
                        return documentoNuevo;
                    }
                    throw new Exception("No se pudo guardar el doc");
                }
                
                

                
            }
        }
    }
}
