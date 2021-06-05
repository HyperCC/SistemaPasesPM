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
                string[] tipoDocumetos = { "ADMIN", 
                    "HSEQ", 
                    "JEFEO_PERACIONES", 
                    "CONTACTO", 
                    "OPIP" };

            }
            else
                Console.WriteLine("YA EXISTEN LOS TIPOS DE DOCUMENTO INICIALES");
        }
    }
}