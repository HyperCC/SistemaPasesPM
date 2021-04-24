using Dominio.Entidades;
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
using System.Threading.Tasks;

namespace WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // configuracion para agregar el context al servicio web
            services.AddDbContext<SistemaPasesContext>(opt =>
                opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

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
