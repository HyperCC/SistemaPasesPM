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
        public static async Task AgregarDocumentosEmpresa(DocumentosEmpresaContratistaRequest request,
            SistemaPasesContext context,
            Guid currentPaseId,
            Guid currentEmpresaId)
        {
            // agregar asesor de prevencion
            // buscar o agregar una persona por su rut
            Persona buscarPersona = await BuscarOAlmacenarPersona.BuscarOAgregarPersona(context,
                request.Rut,
                request.Nombres,
                request.Apellidos);

            // nuevo asesor de prevencion
            var nuevoContratista = new AsesorPrevencion
            {
                RegistroSns = request.ReistroSNS,
                PersonaId = buscarPersona.PersonaId,
                PaseId = currentPaseId
            };


            // agregar los documentos al context
            foreach (var doc in request.Documentos)
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