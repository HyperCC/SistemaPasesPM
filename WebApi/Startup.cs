using Aplicacion.ConfiguracionLogin;
using Aplicacion.ConfiguracionLogin.Contratos;
using Aplicacion.ConfiguracionLogin.TokenSeguridad;
using AutoMapper;
using Dominio.Entidades;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Persistencia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
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
            identityBuilder.AddRoles<IdentityRole>();
            // Claims comunicando las entidades Usuario y IdentityRole
            identityBuilder.AddClaimsPrincipalFactory<UserClaimsPrincipalFactory<Usuario, IdentityRole>>();

            // instanciar el uso de roles de usuario con los datos pre contrstruidos de IdentityRole
            identityBuilder.AddEntityFrameworkStores<SistemaPasesContext>();
            // Claims comunicando las entidades Usuario y IdentityRole
            identityBuilder.AddSignInManager<SignInManager<Usuario>>();

            // configuracion de los datos de prueba para las migraciones
            services.TryAddSingleton<ISystemClock, SystemClock>();

            // creacion de la llave para la validacion de los controladores con seguridad
            // la palabra secreta debe ser la misma ue en el JwtGenerador en la linea 34
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("a1guna p@labr@ secret4"));
            // habilitar la autenticacion por tokens para obtener datos desde la API
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
            {
                opt.TokenValidationParameters = new TokenValidationParameters
                {
                    // cualquier request del cliente debe ser validado por el proyecto
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    // personas por IP que pueden acceder al servicio
                    ValidateAudience = false,
                    // envio del token falso, ya que no hay seleccion de cliente especificos en ValidateAudience
                    ValidateIssuer = false
                };
            });

            // injeccion de la libreria de seguridad y la interface en applicacion.contratos 
            services.AddScoped<IJwtGenerador, JwtGenerador>();

            // dar a concer por el webApp la clase para reconocer al usuario en sesion acltualmente.
            services.AddScoped<IUsuarioSesion, UsuarioSesion>();

            services.AddMvc(opt =>
            {
                // reuiere ue el ususario este autenticado 
                var policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
                opt.Filters.Add(new AuthorizeFilter(policy));
            })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                // agregar validaciones de fluent validation
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssemblyContaining<Registrar.EjecutaValidacion>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // midleware con los errores personalizados
            app.UseMiddleware<ManejadorErrorMiddleware>();

            if (!env.IsDevelopment())
                // para ambientes de desarrollo
                //app.UseDeveloperExceptionPage();

                // The default HSTS value is 30 days. You may want to change this for 
                // production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            // indicar la inicializacion de la validacion para los resultados de los request de clientes
            app.UseAuthentication();

            // uso de Cors
            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}