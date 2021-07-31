using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// Almacenar documentos relacionados a las personas adjuntas en los pases genericos
    /// </summary>
    public class AlmacenarDocumentoPersonaGenerico
    {
        public static async Task<bool> AgregarDocumento(
        // se reutiliza el modelo DocumentoEmpresa por que tiene la misma estructura de datos
        DocumentoEmpresa documentoPersonasRequest
        , SistemaPasesContext context
        , IHostingEnvironment env
        , Guid currentPersonaContratistaId)
        {
            // buscar tipo de documento
            var currentTipoDocumento = await context.TipoDocumento
                .FirstOrDefaultAsync(t => t.Titulo == documentoPersonasRequest.TipoDocumento);

            // si no existe se agrega el tipo 
            if (currentTipoDocumento == null)
            {
                currentTipoDocumento = new TipoDocumento
                {
                    TipoDocumentoId = new Guid(),
                    Titulo = documentoPersonasRequest.TipoDocumento,
                    Obligatoriedad = documentoPersonasRequest.Obligatoriedad
                };
                await context.TipoDocumento.AddAsync(currentTipoDocumento);
            }

            // crear el nuevo documento 
            Documento nuevoDocumentoPersonaGenerica = new Documento
            {
                DocumentoId = new Guid(),
                Extension = documentoPersonasRequest.Extension,
                PersonaId = currentPersonaContratistaId,
                TipoDocumentoId = currentTipoDocumento.TipoDocumentoId
            };

            // agregar la fecha de venc si es que existe
            if (documentoPersonasRequest.FechaVencimiento != null
                && documentoPersonasRequest.FechaVencimiento.Length > 0)

                nuevoDocumentoPersonaGenerica.FechaVencimiento = Convert
                    .ToDateTime(documentoPersonasRequest.FechaVencimiento);

            // almacenar el archivo recibido desde base 64
            string rutaCompleta = ArchivoEnSistema.GuardarArchivo(documentoPersonasRequest.Documento
                , documentoPersonasRequest.Extension
                , env);

            // agregar el documento a la DB
            nuevoDocumentoPersonaGenerica.RutaDocumento = rutaCompleta;
            await context.Documento.AddAsync(nuevoDocumentoPersonaGenerica);

            return true;
        }
    }
}