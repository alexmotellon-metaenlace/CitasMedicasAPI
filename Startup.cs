using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CitasMedicas.Models;
using CitasMedicas.Data;
using AutoMapper;
using CitasMedicas.Services;

namespace CitasMedicas
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

            services.AddCors();
            services.AddControllers();
            services.AddDbContext<CitasMedicasContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection"));
                options.EnableSensitiveDataLogging();
            });
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IMedicoService, MedicoService>();
            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<ICitaService, CitaService>();
            services.AddScoped<IDiagnosticoService, DiagnosticoService>();

            Console.WriteLine("Startup - ConfigureServices - AddDbContext & AddControllers");
            // services.AddSwaggerGen(c =>
            // {
            //     c.SwaggerDoc("v1", new OpenApiInfo { Title = "CitasMedicas", Version = "v1" });
            // });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt =>
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
