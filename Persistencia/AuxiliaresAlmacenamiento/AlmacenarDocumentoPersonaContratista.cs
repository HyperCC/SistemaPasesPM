using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Dominio.Auxiliares.ModelosPaseContratista;
using Microsoft.EntityFrameworkCore;
using Dominio.Entidades;
using Microsoft.AspNetCore.Hosting;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// Almacenar documentos relacionados a las personas adjuntas en pase contratista
    /// </summary>
    public class AlmacenarDocumentoPersonaContratista
    {
        public static async Task<bool> AgregarDocumento(
            DocumentoPersonaContratista documentoPersonasRequest
            , SistemaPasesContext context
            , IHostingEnvironment env
            , Guid currentPaseId
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
            Documento nuevoDocumentoPersonaContratista = new Documento
            {
                DocumentoId = new Guid(),
                Extension = documentoPersonasRequest.Extension,
                PaseId = currentPaseId,
                PersonaId = currentPersonaContratistaId,
                TipoDocumentoId = currentTipoDocumento.TipoDocumentoId
            };

            // agregar la fecha de venc si es que existe
            if (documentoPersonasRequest.FechaVencimiento != null
                && documentoPersonasRequest.FechaVencimiento.Length > 0)

                nuevoDocumentoPersonaContratista.FechaVencimiento = Convert
                    .ToDateTime(documentoPersonasRequest.FechaVencimiento);

            // almacenar el archivo recibido desde base 64
            string rutaCompleta = ArchivoEnServer.GuardarArchivo(documentoPersonasRequest.Documento
                , documentoPersonasRequest.Extension
                , context
                , env);

            // verificar si se recibio un anexo de contrato
            if (documentoPersonasRequest.Descripcion != null)
                if (documentoPersonasRequest.Descripcion.Length > 0)
                {
                    await context.AnexoContrato.AddAsync(new AnexoContrato
                    {
                        Descripcion = documentoPersonasRequest.Descripcion,
                        DocumentoId = nuevoDocumentoPersonaContratista.DocumentoId
                    });
                }

            // verificar si se recibio un registro de persona (RIOHS - ODI - EPPs)
            if (documentoPersonasRequest.FechaRegistro != null)
                if (documentoPersonasRequest.FechaRegistro.Length > 0)
                {
                    await context.RegistroPersona.AddAsync(new RegistroPersona
                    {
                        // TODO: verificar el registro de fechas 
                        FechaRegistro = Convert.ToDateTime(documentoPersonasRequest.FechaRegistro),
                        DocumentoId = nuevoDocumentoPersonaContratista.DocumentoId
                    });
                }

            // agregar el documento a la DB
            nuevoDocumentoPersonaContratista.RutaDocumento = rutaCompleta;
            await context.Documento.AddAsync(nuevoDocumentoPersonaContratista);

            return true;
        }
    }
}
