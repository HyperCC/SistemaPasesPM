using Dominio.Auxiliares.Pases;
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
    public static class AlmacenarDocumentosEmpresa
    {
        public static async Task AgregarDocumentosEmpresa(ICollection<DocumentoUnicoContratistaRequest> request,
            SistemaPasesContext context,
            Guid currentPaseId,
            Guid currentEmpresaId)
        {
            // agregar los documentos al context
            foreach (var doc in request)
            {
                // buscar tipo de documento 
                var currentTipoDocumento = await context.TipoDocumento.FirstOrDefaultAsync(t => t.Titulo == doc.Titulo);

                // ingresar el nuevo documento 
                Documento documentoContratista = new Documento
                {
                    DocumentoId = new Guid(),
                    RutaDocumento = doc.RutaGuardado,
                    FechaCaducidad = Convert.ToDateTime(doc.FechaVencimiento),

                    TipoDocumentoId = currentTipoDocumento.TipoDocumentoId,
                    PaseId = currentPaseId,
                    EmpresaId = currentEmpresaId
                };

                await context.Documento.AddAsync(documentoContratista);
            }
        }
    }
}