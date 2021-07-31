using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Seeders
{
    /// <summary>
    /// Tipos de documento iniciales a crear para la base de datos
    /// </summary>
    public class TipoDocumentoSeeder
    {
        /// <summary>
        /// generar los registros de tipos de documento
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static async Task InsertarData(SistemaPasesContext context)
        {
            var existenTipoDoc = await context.TipoDocumento.AnyAsync();
            if (!existenTipoDoc)
            {
                // todos los tipos de documento solicitados
                string[] tipoDocumetos = {
                    "DOCUMENTO INDUCCION",

                    "CRONOGRAMA DE TRABAJO",
                    "CERTIFICADO DE MUTUALIDAD",
                    "CERTIFICADO DE ACCIDENTABILIDAD",
                    "REGLAMENTO INTERNO",

                    "MATRIZ DE RIESGOS",
                    "PROCEDIMIENTO TRABAJO SEGURO",
                    "PROGRAMA PREVENCION DE RIESGOS",
                    "HDS SUSTANCIAS PELIGROSAS",

                    "CONTRATO DE TRABAJO",
                    "ANEXO DE CONTRATO",
                    "REGITRO RIOHS",
                    "REGISTRO ODI",
                    "REGITRO EPPS",

                    "BASICO",
                    "ALTURA FISICA",
                    "ESPACIOS CONFINADOS",
                    "PSICOSENSOMETRICO",

                    "SOLADOR CALIFICADO",
                    "TRABAJO EN ALTURA",
                    "OPERADOR DE EQUIPO MOVIL",
                    "RIGGER PORTALONERO",
                    "OTROS"
                };

                string[] tipoDocumetoNoObligatorio = {
                    "BASICO",
                    "ALTURA FISICA",
                    "ESPACIOS CONFINADOS",
                    "PSICOSENSOMETRICO",

                    "SOLADOR CALIFICADO",
                    "TRABAJO EN ALTURA",
                    "OPERADOR DE EQUIPO MOVIL",
                    "RIGGER PORTALONERO",
                    "OTROS"
                };

                string[] tiposDocumentoMultiples = {
                    "DOCUMENTO INDUCCION",
                    "ANEXO DE CONTRATO",
                    "PROCEDIMIENTO TRABAJO SEGURO",
                    "HDS SUSTANCIAS PELIGROSAS",
                    "OTROS"
                };

                // agregar todos los tipos de documentos al context
                for (int index = 0; index < tipoDocumetos.Length; index++)
                {
                    TipoDocumento nuevoTipo = new TipoDocumento
                    {
                        TipoDocumentoId = new Guid(),
                        Titulo = tipoDocumetos[index],
                        Obligatoriedad = tipoDocumetoNoObligatorio.Contains(tipoDocumetos[index])
                            ? false : true,
                        IsUnique = tiposDocumentoMultiples.Contains(tipoDocumetos[index])
                            ? false : true
                    };
                    await context.TipoDocumento.AddAsync(nuevoTipo);
                }
                await context.SaveChangesAsync();
            }
            else
                Console.WriteLine("YA EXISTEN LOS TIPOS DE DOCUMENTO INICIALES");
        }
    }
}