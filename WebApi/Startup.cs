using Aplicacion.ConfiguracionLogin;
using Dominio.Entidades;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using WebApi.Middleware;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // acceso a la api desde cualquier origen
            services.AddCors(options =>
            {
                options.AddPolicy(MyAllowSpecificOrigins,
                    builderer =>
                    {
                        builderer.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                    });
            });

            // configuracion para agregar el context al servicio web
            services.AddDbContext<SistemaPasesContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            // configura el mediador para toda la app
            services.AddMediatR(typeof(Registrar.Manejador).Assembly);
            //services.AddMediatR(Assembly.GetExecutingAssembly());

            // configuracion de core identity para el acceso por logins 
            var builder = services.AddIdentityCore<Usuario>();
            // inicializacion del modulo de identidad utilizando a usuario para los login
            var identityBuilder = new IdentityBuilder(builder.UserType, builder.Services);

            // instanciar el uso de roles de usuario con los datos pre contrstruidos de IdentityRole
            identityBuilder.AddEntityFrameworkStores<SistemaPasesContext>();
            // Claims comunicando las entidades Usuario y IdentityRole
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();

            // configuracion de los datos de prueba para las migraciones
            services.TryAddSingleton<ISystemClock, SystemClock>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // uso de Cors
            app.UseCors(MyAllowSpecificOrigins);

            // nuevo midleware con los errores personalizados
            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            else
            {
                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
