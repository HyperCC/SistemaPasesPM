using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistencia.Seeders
{
    public class TipoDocumentoSeeder
    {
        public static async Task InsertarData(SistemaPasesContext context)
        {
            var existenTipoDoc = await context.TipoDocumento.AnyAsync();
            if (!existenTipoDoc)
            {
                string[] tipoDocumetos = {
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

                for (int index = 0; index < tipoDocumetos.Length; index++)
                {
                    var searchTipo = await context.TipoDocumento
                        .FirstOrDefaultAsync(t => t.Titulo == tipoDocumetos[index]);

                    if (searchTipo == null)
                    {
                        TipoDocumento nuevoTipo = new TipoDocumento
                        {
                            TipoDocumentoId = new Guid(),
                            Titulo = tipoDocumetos[index],
                            Obligatoriedad = tipoDocumetoNoObligatorio.Contains(tipoDocumetos[index])
                            ? false
                            : true
                        };
                        await context.TipoDocumento.AddAsync(nuevoTipo);
                    }
                    else
                        Console.WriteLine($"YA EXISTE EL TIPO DE DOCUMENTO {tipoDocumetos[index]}");

                }
                await context.SaveChangesAsync();

            }
            else
                Console.WriteLine("YA EXISTEN LOS TIPOS DE DOCUMENTO INICIALES");
        }
    }
}