using Dominio.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
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
                    "CONTRATO DE TRABAJO",
                    "ANEXO CONTRATO",
                    "RIOHS",
                    "ODI",
                    "EPP",
                    "BASICO",
                    "ALTURA FISICA",
                    "ESPACIOS CONFINADOS",
                    "PSICOSENSOMETRICO",
                    "SOLADOR CALIFICADO",
                    "TRABAJO EN ALTURA",
                    "OPERADOR EQUIPO MOVIL",
                    "RIGGER",
                    "OTROS"
                };
                bool[] obligatoriedad =
                {
                    true,false,true,true,true,false,false,false,false,false,false,false,false,false
                };

                for(int index = 0; index < tipoDocumetos.Length; index++)
                {
                    var searchTipo = await context.TipoDocumento.FirstOrDefaultAsync(t => t.Titulo == tipoDocumetos[index]);

                    if (searchTipo == null)
                    {
                        TipoDocumento nuevoTipo = new TipoDocumento
                        {
                            TipoDocumentoId = new Guid(),
                            Titulo = tipoDocumetos[index],
                            Obligatoriedad = obligatoriedad[index]
                        };
                        await context.TipoDocumento.AddAsync(nuevoTipo);
                    }
                    else
                    {
                        Console.WriteLine($"YA EXISTE EL TIPO DE DOCUMENTO {tipoDocumetos[index]}");
                    }
                }

                await context.SaveChangesAsync();

            }
            else
                Console.WriteLine("YA EXISTEN LOS TIPOS DE DOCUMENTO INICIALES");
        }
    }
}