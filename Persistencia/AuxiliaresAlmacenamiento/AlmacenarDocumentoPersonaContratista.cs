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
    public class AlmacenarDocumentoPersonaContratista
    {
        public static async Task AgregarDocumento(DocumentoPersonaContratista documentoPersonasRequest,
            SistemaPasesContext context, 
            IHostingEnvironment env,
            Guid currentPaseId,
            Guid currentPersonaExternaId)
        {

            var currentTipoDocumento = await context.TipoDocumento.FirstOrDefaultAsync(t => t.Titulo == documentoPersonasRequest.TipoDocumento);
            //TO-DO: Ver que hacer cuando el tipo de documento no se encuentra en la base de datos. Caso que no deberia ocurrir


            Documento nuevoDocumentoContratista = new Documento
            {
                DocumentoId = new Guid(),
                PaseId = currentPaseId,
                Persona = currentPersonaExternaId,
                TipoDocumentoId = currentTipoDocumento.TipoDocumentoId
            };
            //Agregamos la fecha de venc si es que existe
            if (documentoPersonasRequest.FechaVencimiento != null && documentoPersonasRequest.FechaVencimiento.Length > 0)
                nuevoDocumentoContratista.FechaVencimiento = Convert.ToDateTime(documentoPersonasRequest.FechaVencimiento);

            await ArchivoEnServer.guardarArchivo(documentoPersonasRequest.Documento, nuevoDocumentoContratista, env, context);

            //ANEXO DE CONTRATO
            if (documentoPersonasRequest.Descripcion != null)
            {
                AnexoContrato documentoAnexo = new AnexoContrato
                {
                    AnexoContratoId = new Guid(),
                    DocumentoId = nuevoDocumentoContratista.DocumentoId,
                    Descripcion = documentoPersonasRequest.Descripcion
                };

                await context.AnexoContrato.AddAsync(documentoAnexo);
            }
            else
            {
                //REGISTRO PERSONA
                if (documentoPersonasRequest.FechaRegistro != null)
                {
                    RegistroPersona registroPersona = new RegistroPersona
                    {
                        RegistroPersonaId = new Guid(),
                        FechaRegistro = Convert.ToDateTime(documentoPersonasRequest.FechaRegistro),
                        DocumentoId = nuevoDocumentoContratista.DocumentoId
                    };

                    await context.RegistroPersona.AddAsync(registroPersona);
                }
                
            }
            var result = await context.SaveChangesAsync();


        }
    }
}
