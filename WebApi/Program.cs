using Dominio.Entidades;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Persistencia;
using Persistencia.Seeders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var hostserver = CreateWebHostBuilder(args).Build();

            // agregar las migraciones creadas con EF
            using (var ambiente = hostserver.Services.CreateScope())
            {
                var services = ambiente.ServiceProvider;
                try
                {
                    var usuarioManager = services.GetRequiredService<UserManager<Usuario>>();
                    // llamar el uso de CursosOnlineContext
                    var context = services.GetRequiredService<SistemaPasesContext>();
                    // hacer la migracion directamente a la DB de no existir las tablas
                    context.Database.Migrate();

                    // instancia del seeder de datos creado en Persistencia
                    UsuarioSeeder.InsertarData(context, usuarioManager).Wait();
                }
                catch (Exception ex)
                {
                    // lanzar log con los errores encontrados durante la migracion en la DB
                    var logging = services.GetRequiredService<ILogger<Program>>();
                    logging.LogError(ex, "Ocurrio un error durante la migracion y no se efectu� correctamente.");
                }

                hostserver.Run();
            }

        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
