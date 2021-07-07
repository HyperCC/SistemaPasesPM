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

namespace Persistencia.AuxiliaresAlmacenamiento
{
    /// <summary>
    /// clase auxiliar para almacenar los nombres y apellidos por persona
    /// </summary>
    public static class AlmacenarDocumentosEmpresa
    {
        public static async Task AgregarDocumentosEmpresa(DocumentoEmpresaContratista request
            , SistemaPasesContext context
            , IHostingEnvironment env
            , Guid currentPaseId
            , Guid currentEmpresaId)
        {

            // buscar tipo de documento 
            var currentTipoDocumento = await context.TipoDocumento
                .FirstOrDefaultAsync(t => t.Titulo == request.TipoDocumento);

            // ingresar el nuevo requestumento 
            Documento nuevoDocumentoContratista = new Documento
            {
                DocumentoId = new Guid(),
                PaseId = currentPaseId,
                EmpresaId = currentEmpresaId,
                TipoDocumentoId = currentTipoDocumento.TipoDocumentoId
            };

            //Agregamos la fecha de venc si es que existe
            if (request.FechaVencimiento != null && request.FechaVencimiento.Length > 0)
                nuevoDocumentoContratista.FechaVencimiento = Convert.ToDateTime(request.FechaVencimiento);

            await ArchivoEnServer.guardarArchivo(request.Documento, nuevoDocumentoContratista, env, context);

            await context.Documento.AddAsync(nuevoDocumentoContratista);

        }
    }
}