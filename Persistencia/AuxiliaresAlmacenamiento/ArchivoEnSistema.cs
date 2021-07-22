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
    public class ArchivoEnSistema
    {
        public static string GuardarArchivo(string archivoBase64
            , string currentExtension
            , IHostingEnvironment env)
        {
            currentExtension = currentExtension.ToLower();
            string[] permittedExtensions = { ".txt", ".pdf", ".docx", ".doc", ".png", ".jpeg", ".jpg", ".ppt", ".pptx", "xls", "xlsx" };

            Console.WriteLine("--------------------------------");
            Console.WriteLine($"SE HA ENTRADO A LA SECCION DE ALMCENAR DOCS CON EXTENSION {currentExtension}.");
            Console.WriteLine("--------------------------------");

            // verificar si la extension es aceptada
            if (!permittedExtensions.Contains(currentExtension))
                throw new Exception("Extension Archivo no permitida");

            // ruta del archivo y extension
            var docname = $"{Guid.NewGuid()}{currentExtension}";
            var documentosFolder = Path.Combine(env.WebRootPath, "Uploads", "Documentos");

            if (!Directory.Exists(documentosFolder))
                Directory.CreateDirectory(documentosFolder);

            // obtener el archivo base64
            byte[] fileCrudo = Convert.FromBase64String(archivoBase64);

            // path completo 
            string completePath = Path.Combine(documentosFolder, docname);

            // almacenar el archivo 
            File.WriteAllBytes(completePath, fileCrudo);
            return completePath;
        }
    }
}