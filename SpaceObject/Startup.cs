using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SpaceObjectApi.Entities;
using SpaceObjectApi.Models;
using SpaceObjectApi.Repository;

namespace SpaceObjectApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ISpaceObjectRepository<Asteroid>, SpaceObjectRepository<Asteroid>>();

            services.AddScoped<ISpaceObjectRepository<BlackHole>, SpaceObjectRepository<BlackHole>>();

            services.AddScoped<ISpaceObjectRepository<Planet>, SpaceObjectRepository<Planet>>();

            services.AddScoped<ISpaceObjectRepository<Star>, SpaceObjectRepository<Star>>();

            services.AddDbContext<SpaceObjectContext>(optionsAction =>
                     optionsAction.UseSqlite("name=ConnectionStrings:DefaultConnection"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SpaceObject.Api", Version = "v1" });
            });

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SpaceObject.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
