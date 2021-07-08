using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Dominio.Entidades;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Persistencia;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    public class ArchivoEnServer
    {
        public static async Task<Documento> guardarArchivo(
                                                           IFormFile archivo,
                                                           Documento doc,
                                                           IHostingEnvironment env,
                                                           SistemaPasesContext context)
        {
            string[] permittedExtensions = { ".txt", ".pdf", ".docx", ".png", ".jpeg" };

            Console.WriteLine("--------------------------------");
            Console.WriteLine("SE HA ENTRADO A LA SECCION DE ALMCENAR DOCS");
            Console.WriteLine("--------------------------------");


            var extensionArchivo = Path.GetExtension(archivo.FileName).ToLowerInvariant();
            var documentosFolder = Path.Combine(env.WebRootPath, "Uploads", "Documentos");

            if (string.IsNullOrEmpty(extensionArchivo) || !permittedExtensions.Contains(extensionArchivo))
            {
                throw new Exception("Extension Archivo no permitida");
            }
            else
            {
                if (!Directory.Exists(documentosFolder))
                {
                    Directory.CreateDirectory(documentosFolder);
                }
                //Archivo no vacio(?)
                if (archivo.Length > 0)
                {
                    var docname = $"{Guid.NewGuid()}.{archivo.FileName}";

                    using (var stream = new FileStream(Path.Combine(documentosFolder, docname), FileMode.Create))
                    {
                        await archivo.CopyToAsync(stream);
                        doc.RutaDocumento = Path.Combine(documentosFolder, docname);
                    }
                }

                await context.Documento.AddAsync(doc);

                return doc;
            }

            //return false;
        }
    }
}
