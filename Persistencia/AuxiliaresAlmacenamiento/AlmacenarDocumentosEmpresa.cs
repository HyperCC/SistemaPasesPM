using Dominio.Auxiliares.Pases;
using Dominio.Auxiliares.ModelosPaseContratista;
using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// clase auxiliar para almacenar los nombres y apellidos por persona
    /// </summary>
    public static class AlmacenarDocumentosEmpresa
    {
        public static async Task<bool> AgregarDocumentosEmpresa(
            DocumentosEmpresaContratista documentoEmpresaContratista
            , SistemaPasesContext context
            , IHostingEnvironment env
            , Guid currentPaseId
            , Guid currentEmpresaId)
        {

            // buscar tipo de documento 
            var currentTipoDocumento = await context.TipoDocumento
                .FirstOrDefaultAsync(t => t.Titulo == documentoEmpresaContratista.TipoDocumento);

            // si no existe se agrega el tipo 
            if (currentTipoDocumento == null)
            {
                currentTipoDocumento = new TipoDocumento
                {
                    TipoDocumentoId = new Guid(),
                    Titulo = documentoEmpresaContratista.TipoDocumento,
                    Obligatoriedad = documentoEmpresaContratista.Obligatoriedad
                };
                await context.TipoDocumento.AddAsync(currentTipoDocumento);
            }

            // ingresar el nuevo requestumento 
            Documento nuevoDocumentoContratista = new Documento
            {
                DocumentoId = new Guid(),
                Extension = documentoEmpresaContratista.Extension,
                PaseId = currentPaseId,
                EmpresaId = currentEmpresaId,
                TipoDocumentoId = currentTipoDocumento.TipoDocumentoId
            };

            // agregar la fecha de venc si es que existe
            if (documentoEmpresaContratista.FechaVencimiento != null
                && documentoEmpresaContratista.FechaVencimiento.Length > 0)

                nuevoDocumentoContratista.FechaVencimiento = Convert
                    .ToDateTime(documentoEmpresaContratista.FechaVencimiento);

            // almacenar el archivo recibido desde base 64
            string rutaCompleta = ArchivoEnServer.GuardarArchivo(documentoEmpresaContratista.Documento
                , documentoEmpresaContratista.Extension
                , context
                , env);

            // agregar el documento a la DB
            nuevoDocumentoContratista.RutaDocumento = rutaCompleta;
            await context.Documento.AddAsync(nuevoDocumentoContratista);

            return true;
        }
    }
}